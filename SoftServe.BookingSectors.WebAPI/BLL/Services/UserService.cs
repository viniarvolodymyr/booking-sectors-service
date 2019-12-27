using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.ErrorHandling;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers.LoggerManager;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System;



namespace SoftServe.BookingSectors.WebAPI.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork database;
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;

        public UserService(IUnitOfWork database, IMapper mapper, ILoggerManager logger)
        {
            this.database = database;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<bool> CheckPasswords(string password, int id)
        {
            var entity = await database.UserRepository.GetEntityByIdAsync(id);
            byte[] passToCheck = SHA256Hash.Compute(password);
            return entity.Password.SequenceEqual(passToCheck);
        }
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await database.UserRepository.GetAllEntitiesAsync();
            var dtos = mapper.Map<IEnumerable<User>, List<UserDTO>>(users);
            return dtos;
        }
        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var entity = await database.UserRepository.GetEntityByIdAsync(id);

            if (entity == null)
            {
                return null;
            }
            var dto = mapper.Map<User, UserDTO>(entity);

            return dto;
        }

        public async Task<UserDTO> GetUserByPhoneAsync(string phone)
        {
            var user = await database.UserRepository
                .GetByCondition(x => x.Phone == phone)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"User with phone number: {phone} not found when trying to get entity.");
            }

            var dto = mapper.Map<User, UserDTO>(user);
            return dto;
        }



        public async Task<User> UpdateUserById(int id, UserDTO userDTO)
        {
            var user = await database.UserRepository.GetEntityByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            user.Firstname = userDTO.Firstname;
            user.Lastname = userDTO.Lastname;
            user.Phone = userDTO.Phone;
            user.ModDate = System.DateTime.Now;

            database.UserRepository.UpdateEntity(user);
            bool isSaved = await database.SaveAsync();

            return (isSaved == true) ? mapper.Map<UserDTO, User>(userDTO) : null;
        }
        public async Task<User> UpdateUserPassById(int id, UserDTO userDTO)
        {
            var user = await database.UserRepository.GetEntityByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            user.ModDate = System.DateTime.Now;
            user.Password = SHA256Hash.Compute(userDTO.Password);

            database.UserRepository.UpdateEntity(user);
            bool isSaved = await database.SaveAsync();

            return (isSaved == true) ? mapper.Map<UserDTO, User>(userDTO) : null;
        }

        public async Task<User> DeleteUserByIdAsync(int id)
        {
            var user = await database.UserRepository.DeleteEntityByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            bool isSaved = await database.SaveAsync();

            return (isSaved == true) ? user.Entity : null;
        }

        public async Task<bool> InsertEmailAsync(int id, string email)
        {
            var getEmail = await database.EmailRepository
                            .GetByCondition(x => x.UserId == id)
                            .FirstOrDefaultAsync();


            Email emailEntity = new Email { UserId = id, Email1 = email };

            if (getEmail == null)
            {
                await database.EmailRepository.InsertEntityAsync(emailEntity);
            }
            else
            {
                emailEntity.Id = getEmail.Id;
                database.EmailRepository.UpdateEntity(emailEntity);
            }


            return (await database.SaveAsync()) ? true : false;

        }
    }
}
