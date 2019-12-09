
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers;

using AutoMapper;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork database;
        private readonly IMapper mapper;
        public UserService(IUnitOfWork database, IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await database.UsersRepository.GetAllEntitiesAsync();
            var dtos = mapper.Map<IEnumerable<User>, List<UserDTO>>(users);
            return dtos;
        }
        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var entity = await database.UsersRepository.GetEntityByIdAsync(id);
            if (entity == null)
            {
                return null;
            }
            var dto = mapper.Map<User, UserDTO>(entity);
            return dto;
        }

        public async Task<UserDTO> GetUserByPhoneAsync(string phone)
        {
            var entities = await database.UsersRepository.GetAllEntitiesAsync();
            int entityId = entities.Where(u => u.Phone == phone)
                                .Select(u => u.Id)
                                    .FirstOrDefault();
            var entity = await database.UsersRepository.GetEntityByIdAsync(entityId);
            if (entity == null)
            {
                return null;
            }
            var dto = mapper.Map<User, UserDTO>(entity);
            return dto;
        }

        public async Task UpdateUserById(int id, UserDTO userDTO)
        {
            var entity = await database.UsersRepository.GetEntityByIdAsync(id);
            entity.Firstname = userDTO.Firstname;
            entity.Lastname = userDTO.Lastname;
            entity.Phone = userDTO.Phone;
            entity.Password = System.Text.Encoding.ASCII.GetBytes(userDTO.Password);
            entity.ModDate = System.DateTime.Now;
            database.UsersRepository.UpdateEntity(entity);
            await database.SaveAsync();
        }

        public async Task InsertUserAsync(UserDTO userDTO)
        {
            var userToInsert = mapper.Map<UserDTO, User>(userDTO);

            string randomPassword = RandomNumbers.Generate();
            userToInsert.Password = SHA256Hash.Compute(randomPassword);
            userToInsert.ModUserId = null;

            await database.UsersRepository.InsertEntityAsync(userToInsert);
            await database.SaveAsync();
        }
        public void Dispose()
        {
            database.Dispose();
        }
    }
}
