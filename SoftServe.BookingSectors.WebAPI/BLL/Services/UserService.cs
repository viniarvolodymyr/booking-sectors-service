using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.BLL.Interfaces;
using AutoMapper;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork Database;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await Database.UsersRepository.GetAllEntitiesAsync();
            var dtos = _mapper.Map<IEnumerable<User>, List<UserDTO>>(users);
            return dtos;
        }
        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var entity = await Database.UsersRepository.GetEntityByIdAsync(id);
            if (entity == null)
            {
                return null;
            }
            var dto = _mapper.Map<User, UserDTO>(entity);
            return dto;
        }

        public async Task<UserDTO> GetUserByPhoneAsync(string phone)
        {
            var entities = await Database.UsersRepository.GetAllEntitiesAsync();
            int entityId = entities.Where(u => u.Phone == phone)
                                .Select(u => u.Id)
                                    .FirstOrDefault();
            var entity = await Database.UsersRepository.GetEntityByIdAsync(entityId);
            if (entity == null)
            {
                return null;
            }
            var dto = _mapper.Map<User, UserDTO>(entity);
            return dto;
        }

        public async Task UpdateUserById(int id, UserDTO userDTO)
        {
            var entity = await Database.UsersRepository.GetEntityByIdAsync(id);
            entity.Firstname = userDTO.Firstname;
            entity.Lastname = userDTO.Lastname;
            entity.Phone = userDTO.Phone;
            entity.Password = System.Text.Encoding.ASCII.GetBytes(userDTO.Password);
            entity.ModDate = System.DateTime.Now;
            Database.UsersRepository.UpdateEntity(entity);
            await Database.SaveAsync();
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
