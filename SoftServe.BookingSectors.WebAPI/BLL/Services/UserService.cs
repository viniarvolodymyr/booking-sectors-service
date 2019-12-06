using System;
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
        public async Task<IEnumerable<UserDTO>> GetAllSectorsAsync()
        {
            var users = await Database.Users.GetAllEntitiesAsync();
            var dtos = _mapper.Map<IEnumerable<User>, List<UserDTO>>(users);
            return dtos;
        }
        public async Task<UserDTO> GetSectorByIdAsync(int id)
        {
            var entity = await Database.Users.GetEntityAsync(id);
            if (entity == null)
            {
                return null;
            }
            var dto = _mapper.Map<User, UserDTO>(entity);
            return dto;
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
