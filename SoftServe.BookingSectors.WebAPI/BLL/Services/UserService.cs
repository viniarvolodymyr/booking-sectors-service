using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers.LoggerManager;

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
            var entities = await database.UserRepository.GetAllEntitiesAsync();
            int entityId = entities.Where(u => u.Phone == phone)
                                .Select(u => u.Id)
                                    .FirstOrDefault();
            var entity = await database.UserRepository.GetEntityByIdAsync(entityId);
            if (entity == null)
            {
                return null;
            }
            var dto = mapper.Map<User, UserDTO>(entity);
            return dto;
        }

        public async Task UpdateUserById(int id, UserDTO userDTO)
        {
            var entity = await database.UserRepository.GetEntityByIdAsync(id);
            entity.Firstname = userDTO.Firstname;
            entity.Lastname = userDTO.Lastname;
            entity.Phone = userDTO.Phone;
            entity.Password = System.Text.Encoding.ASCII.GetBytes(userDTO.Password);
            entity.ModDate = System.DateTime.Now;
            database.UserRepository.UpdateEntity(entity);
            await database.SaveAsync();
        }

        public async Task InsertUserAsync(UserDTO userDTO)
        {
            var userToInsert = mapper.Map<UserDTO, User>(userDTO);
            string randomPassword = RandomNumbers.Generate();
            userToInsert.Password = SHA256Hash.Compute(randomPassword);
            userToInsert.ModUserId = null;

            await database.UserRepository.InsertEntityAsync(userToInsert);
            await database.SaveAsync();
        }
    }
}
