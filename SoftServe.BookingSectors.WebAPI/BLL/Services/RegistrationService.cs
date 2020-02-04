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

        private readonly IUserService userService;

        public RegistrationService(IUnitOfWork database, IMapper mapper, ILoggerManager logger, IUserService userService)
        {
            this.database = database;
            this.mapper = mapper;
            this.userService = userService;
        }
        

        public async Task<UserDTO> UpdateUserById(int id, UserDTO userDTO)
        {
            var existedUser = await database.UserRepository.GetEntityByIdAsync(id);
            if (existedUser == null)
            {
                return null;
            }

            existedUser.Firstname = userDTO.Firstname;
            existedUser.Lastname = userDTO.Lastname;
            existedUser.Phone = userDTO.Phone;
            existedUser.Email = userDTO.Email;

            var updatedUser = database.UserRepository.UpdateEntity(existedUser);
            bool isSaved = await database.SaveAsync();

            return isSaved ?
                mapper.Map<User, UserDTO>(updatedUser) :
                null;
        }

        public async Task<UserDTO> InsertUserAsync(UserDTO userDTO)
        {
            string inputEmail = userDTO.Email.Trim();
            var existingEmail = await GetUserByEmailAsync(inputEmail);

            if (existingEmail != null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Conflict,
                    $"User with email: {inputEmail}, Already exists.");
            }


            string inputPassword = (IsNullOrEmpty(userDTO.Password)) ?
                                    RandomNumbers.Generate() :
                                    userDTO.Password;


            var insertUser = mapper.Map<UserDTO, User>(userDTO);


            var existingUser = await userService.GetUserByPhoneAsync(userDTO.Phone);
            if( existingUser != null 
                && existingUser.RoleId == (int)UserRolesEnum.Guest )
            {
                  existingUser.Email = userDTO.Email;
                  existingUser.RoleId = (int)UserRolesEnum.User;
                  
            }

            insertUser.Password = SHA256Hash.Compute(inputPassword);
            insertUser.RoleId = 2;

            var insertedUser = await database.UserRepository.InsertEntityAsync(insertUser);
            bool isSaved = await database.SaveAsync();

            if (!isSaved)
            {
                return null;
            }
            
            //Generate hash data for confirm email
            string hashConfirmData = EmailConfirmHelper.GetHash(insertedUser.Id, insertUser.IsEmailValid, insertUser.Email);
            string linkConfirm =
                EmailConfirmHelper.GetLink("https://bookingsectors.azurewebsites.net", insertUser.Email, hashConfirmData);

            //Send email

            string emailMessage =
                EmailBody.Registration.GetBodyMessage(insertUser.Lastname, insertedUser.Phone, linkConfirm);

            EmailSender sender = new EmailSender(emailMessage);

            await sender.SendAsync("Registration in Booking Fishing Sectors",
                         inputEmail,
                         $"{insertUser.Lastname} {insertUser.Firstname}");

            return mapper.Map<User, UserDTO>(insertedUser);
        }

        public async Task<UserDTO> InsertGuestUserAsync(UserDTO userDTO)
        {
            var insertUser = mapper.Map<UserDTO, User>(userDTO);
            insertUser.RoleId = 3;

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


            if(isHashEqual)
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
