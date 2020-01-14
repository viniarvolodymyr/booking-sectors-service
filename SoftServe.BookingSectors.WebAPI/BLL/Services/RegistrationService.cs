using System;
using System.Linq;
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
            insertUser.Password = SHA256Hash.Compute(inputPassword);
            insertUser.RoleId = 2;

            var insertedUser = await database.UserRepository.InsertEntityAsync(insertUser);
            bool isSaved = await database.SaveAsync();

            if (!isSaved)
            {
                return null;
            }

            EmailSender sender = new EmailSender($"Hello, {insertUser.Lastname}." +
                                                 $" You write a site for Booking Fishing sectors. {Environment.NewLine}" +
                                                 $" Your login: {insertedUser.Phone} {Environment.NewLine}" +
                                                 $" Your password: {inputPassword} {Environment.NewLine} Have a nice day :) ");

            await sender.SendAsync("Registration in Booking Fishing Sectors",
                         inputEmail,
                         $"{insertUser.Lastname} {insertUser.Firstname}");

            return mapper.Map<User, UserDTO>(insertedUser);
        }

        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            var user = await database.UserRepository
                .GetByCondition(x => x.Email == email)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            return mapper.Map<User, UserDTO>(user);
        }
        
        public async Task<bool> ResetPasswordAsync(UserDTO userDTO){
            string value =  
            + userDTO.Id + "#"
            + userDTO.Password.ToString();

            var d = value;


            return true;
        }
        public async Task<UserDTO> SetNewPassword(UserDTO userDTO, string Hash){
            return new UserDTO();
        }
    }
    public static class ResetPasswordLink{

    }

}
