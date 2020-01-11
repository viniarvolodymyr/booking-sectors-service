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
            user.Email = userDTO.Email;

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

            user.Password = SHA256Hash.Compute(userDTO.Password);

            database.UserRepository.UpdateEntity(user);
            bool isSaved = await database.SaveAsync();

            return (isSaved == true) ? mapper.Map<UserDTO, User>(userDTO) : null;
        }

        public async Task<User> UpdateUserPhotoById(int id, IFormFile formFile)
        {
            var user = await database.UserRepository.GetEntityByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);

                // Upload the file if less than 2 MB
                if (memoryStream.Length < 2097152)
                {
                    user.Photo = memoryStream.ToArray();
                    database.UserRepository.UpdateEntity(user);
                    bool isSaved = await database.SaveAsync();
                    return (isSaved == true) ? user : null;
                }
                else
                {
                    return null;
                    //  ModelState.AddModelError("File", "The file is too large.");
                }
            }



        }

        public async Task<IFormFile> GetUserPhotoById(int id)
        {
            var entity = await database.UserRepository.GetEntityByIdAsync(id);

            if (entity == null)
            {
                return null;
            }

            FormFile file;
            using (var ms = new MemoryStream(entity.Photo))
            {
                File.WriteAllBytes("test.jpg", entity.Photo);
                file = new FormFile(ms, 0, ms.Length, "file.jpg", "file")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg",

                };

                return file;
            }
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
    }
}
