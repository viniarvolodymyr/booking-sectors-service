using System.Net;
using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers.LoggerManager;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftServe.BookingSectors.WebAPI.BLL.ErrorHandling;
using static System.String;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUnitOfWork database;
        private readonly IMapper mapper;

        public RegistrationService(IUnitOfWork database, IMapper mapper, ILoggerManager logger)
        {
            this.database = database;
            this.mapper = mapper;
        }


        public async Task<UserDTO> InsertUserAsync(UserDTO userDTO)
        {
            // Check email
            string inputEmail = userDTO.Email.Trim();
            var existingEmail = await GetUserByEmailAsync(inputEmail);

            if (existingEmail != null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Conflict,
                    $"User with email: {inputEmail}, Already exists.");
            }

            // Password generate
            string inputPassword = (IsNullOrEmpty(userDTO.Password)) ?
                                    RandomNumbers.Generate() :
                                    userDTO.Password;


            // Get data
            var insertUser = mapper.Map<UserDTO, User>(userDTO);
            insertUser.Password = SHA256Hash.Compute(inputPassword);

            // Update user (from guest)
            var existingUser =  await database.UserRepository
                .GetByCondition(x=>x.Phone == userDTO.Phone)
                .FirstOrDefaultAsync();

            // User data after update/insert
            User insertedUser = new User();

            if (existingUser != null
                && existingUser.RoleId == (int)UserRolesEnum.Guest)
            {
                existingUser.Email = userDTO.Email;
               existingUser.Role = null;
                existingUser.RoleId = (int)UserRolesEnum.User;
                existingUser.Password = insertUser.Password;

                insertedUser = database.UserRepository.UpdateEntity(existingUser);
            }
            else
            {
                insertUser.RoleId = (int)UserRolesEnum.User;
                insertedUser = await database.UserRepository.InsertEntityAsync(insertUser);
            }

            bool isSaved = await database.SaveAsync();

            if (!isSaved)
            {
                return null;
            }

            // Send email
            await SendEmail(insertedUser, insertUser, inputEmail);


            return mapper.Map<User, UserDTO>(insertedUser);
        }

        public async Task SendEmail(User beforeInsertUserData, User afterInsertUserData, string email){
             //Generate hash data for confirm email
            string hashConfirmData =
                EmailConfirmHelper.GetHash(beforeInsertUserData.Id, afterInsertUserData.IsEmailValid, afterInsertUserData.Email);
            string linkConfirm =
                EmailConfirmHelper.GetLink("https://bookingsectors.azurewebsites.net", afterInsertUserData.Email, hashConfirmData);

            //Send email
            string emailMessage =
                EmailBody.Registration.GetBodyMessage(afterInsertUserData.Lastname, beforeInsertUserData.Phone, linkConfirm);

            EmailSender sender = new EmailSender(emailMessage);

            await sender.SendAsync("Registration in Booking Fishing Sectors",
                         email,
                         $"{afterInsertUserData.Lastname} {afterInsertUserData.Firstname}");

        }
        
       
        public async Task<UserDTO> InsertGuestUserAsync(UserDTO userDTO)
        {
            var insertUser = mapper.Map<UserDTO, User>(userDTO);
            insertUser.RoleId = (int)UserRolesEnum.Guest;
            insertUser.Email = null;
            insertUser.Password = null;

            var insertedUser = await database.UserRepository.InsertEntityAsync(insertUser);
            bool isSaved = await database.SaveAsync();

            return isSaved
                ? mapper.Map<User, UserDTO>(insertedUser)
                : null;
        }

        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            var user = await database.UserRepository
                .GetByCondition(x => x.Email == email)
                .FirstOrDefaultAsync();

            return user == null
                ? null
                : mapper.Map<User, UserDTO>(user);
        }

        public async Task<bool> ConfirmEmailAsync(UserDTO userDTO, string hash)
        {
            string hashConfirmData = EmailConfirmHelper.GetHash(userDTO.Id, userDTO.IsEmailValid, userDTO.Email);
            bool isHashEqual = EmailConfirmHelper.EqualHash(hash, hashConfirmData);


            if (isHashEqual)
            {
                var existedUser = await database.UserRepository.GetEntityByIdAsync(userDTO.Id);

                existedUser.IsEmailValid = true;
                var updatedUser = database.UserRepository.UpdateEntity(existedUser);
                bool isSave = await database.SaveAsync();


                return isSave;

            }

            return false;
        }

    }
}
