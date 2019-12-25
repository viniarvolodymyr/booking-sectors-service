using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<UserDTO> GetUserByPhoneAsync(string phone);
        Task<User> UpdateUserById(int id, UserDTO userDTO);
        Task<UserDTO> InsertUserAsync(UserDTO userDTO);
        Task<User> DeleteUserByIdAsync(int id);
        Task<bool> CheckPasswords(string password, int id);
    }
}
