using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.ErrorHandling;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers.LoggerManager;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using System.Threading.Tasks;
using System;
using System.Net;



namespace SoftServe.BookingSectors.WebAPI.BLL.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUnitOfWork database;
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;

        public RegistrationService(IUnitOfWork database, IMapper mapper, ILoggerManager logger)
        {
            this.database = database;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<RegistrationDTO> InsertUserAsync(RegistrationDTO userDTO)
        {
            string inputPassword = (String.IsNullOrEmpty(userDTO.Password)) ?
                                    RandomNumbers.Generate() :
                                    userDTO.Password;

            string inputEmail = userDTO.Email.Trim();

            var insertedUser = mapper.Map<RegistrationDTO, User>(userDTO);
            insertedUser.Password = SHA256Hash.Compute(inputPassword);
            insertedUser.ModUserId = null;
            insertedUser.RoleId = 2;

            await database.UserRepository.InsertEntityAsync(insertedUser);
            bool isSaved = await database.SaveAsync();




            if (!isSaved)
            {
                return null;
            }

            var newUser = mapper.Map<User, RegistrationDTO>(insertedUser);

            var email = InsertEmailAsync(newUser.Id, inputEmail);

            if (!email.Result)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, $"Email: {inputEmail} not added");
            }

            EmailSender sender = new EmailSender($"Hello, {insertedUser.Lastname}." +
                                                 $" You write a site for Booking Fishing sectors. {Environment.NewLine}" +
                                                 $" Your password: {inputPassword} {Environment.NewLine} Have a nice day :) ");

            await sender.SendAsync("Registration in Booking Fishing Sectors",
                         inputEmail,
                         $"{insertedUser.Lastname} {insertedUser.Firstname}");

            return newUser;
        }

        public async Task<bool> InsertEmailAsync(int id, string email)
        {
            var getEmail = await database.EmailRepository
                            .GetByCondition(x => x.UserId == id)
                            .FirstOrDefaultAsync();

            if (getEmail == null)
            {
                Email emailEntity = new Email { UserId = id, Email1 = email };

                await database.EmailRepository.InsertEntityAsync(emailEntity);
                var result = await database.SaveAsync();
                return (result) ? true : false;
            }

            return false;
        }
    }
}
