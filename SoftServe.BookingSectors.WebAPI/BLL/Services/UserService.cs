using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers.LoggerManager;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;


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

        #region Get
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await database.UserRepository.GetAllEntitiesAsync();

            return mapper.Map<IEnumerable<User>, List<UserDTO>>(users);
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var entity = await database.UserRepository.GetEntityByIdAsync(id);
            if (entity == null)
            {
                return null;
            }

            var dto = mapper.Map<User, UserDTO>(entity);

            if (entity.Photo != null)
            {
                dto.Photo = Convert.ToBase64String(entity.Photo);
            }

            return dto;
        }

        public async Task<UserDTO> GetUserByPhoneAsync(string phone)
        {
            var user = await database.UserRepository
                .GetByCondition(x => x.Phone == phone)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            var dto = mapper.Map<User, UserDTO>(user);

            if (user.Photo != null)
            {
                dto.Photo = Convert.ToBase64String(user.Photo);
            }

            return dto;
        }
        public async Task<UserDetailsDTO> GetUserDetailsAsync(int id)
        {
            var userDetail = await database.UserRepository.GetEntityByIdAsync(id);

            if(userDetail == null)
            {
                return null;
            }

            var dto = mapper.Map<User, UserDetailsDTO>(userDetail);

            return dto;
        }
        #endregion


        #region Update

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

        public async Task<UserDTO> UpdateUserPassById(int id, UserDTO userDTO)
        {
            var existedUser = await database.UserRepository.GetEntityByIdAsync(id);
            if (existedUser == null)
            {
                return null;
            }

            existedUser.Password = SHA256Hash.Compute(userDTO.Password);

            var updatedUser = database.UserRepository.UpdateEntity(existedUser);
            bool isSaved = await database.SaveAsync();

            return isSaved ?
                mapper.Map<User, UserDTO>(updatedUser) :
                null;
        }

        public async Task<UserDTO> UpdateUserPhotoById(int id, IFormFile formFile)
        {
            var existedUser = await database.UserRepository.GetEntityByIdAsync(id);
            if (existedUser == null)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);

                if (memoryStream.Length < 2097152)
                {
                    existedUser.Photo = memoryStream.ToArray();
                    var updatedUser = database.UserRepository.UpdateEntity(existedUser);
                    bool isSaved = await database.SaveAsync();

                    return isSaved ?
                        mapper.Map<User, UserDTO>(updatedUser) :
                        null;
                }

                return null;
            }
        }

        public async Task<UserDTO> DeleteUserPhotoById(int id)
        {
            var existedUser = await database.UserRepository.GetEntityByIdAsync(id);
            if (existedUser == null)
            {
                return null;
            }

            existedUser.Photo = null;

            var updatedUser = database.UserRepository.UpdateEntity(existedUser);
            bool isSaved = await database.SaveAsync();

            return isSaved ?
                mapper.Map<User, UserDTO>(updatedUser) :
                null;
        }

        #endregion

        public async Task<UserDTO> DeleteUserByIdAsync(int id)
        {
            var user = await database.UserRepository.DeleteEntityByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            bool isSaved = await database.SaveAsync();


            return (isSaved) ?
                mapper.Map<User, UserDTO>(user) :
                null;
        }

        #region Password
        public async Task<bool> ResetPassword(UserDTO userDTO)
        { 
            var existedUser = await database.UserRepository.GetEntityByIdAsync(userDTO.Id);
            if (existedUser == null)
            {
                return false;
            }

            string newPass = RandomNumbers.Generate();

            EmailSender sender = new EmailSender($"Hello, {userDTO.Firstname}." +
                                             $" Your new password: <br>" +
                                             $" <b>{newPass}</b> <br> " +
                                             $" If you want, you can change it in your profile. <br> " +
                                             $" Have a nice day :) ");

            await sender.SendAsync("Reset password on BookingSector",
                userDTO.Email,
                $"{userDTO.Lastname} {userDTO.Firstname}");

            existedUser.Password = SHA256Hash.Compute(newPass);

            var updatedUser = database.UserRepository.UpdateEntity(existedUser);
            bool isSaved = await database.SaveAsync();

            return isSaved ?
                true :
                false;
        }

        public async Task<bool> CheckPasswords(string password, int id)
        {
            var entity = await database.UserRepository.GetEntityByIdAsync(id);
            byte[] passToCheck = SHA256Hash.Compute(password);

            return entity.Password.SequenceEqual(passToCheck);
        }
        #endregion


    }
}
