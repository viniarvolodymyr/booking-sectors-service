using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<UserDTO> GetUserByPhoneAsync(string phone);
        Task<UserDetailsDTO> GetUserDetailsAsync(int id);
        Task<UserDTO> UpdateUserById(int id, UserDTO userDTO);
        Task<UserDTO> UpdateUserPassById(int id, UserDTO userDTO);
        Task<UserDTO> DeleteUserByIdAsync(int id);
        Task<bool> CheckPasswords(string password, int id);
        Task<UserDTO> UpdateUserPhotoById(int id, IFormFile image);
        Task<UserDTO> DeleteUserPhotoById(int id);
        Task<bool> ResetPassword(UserDTO user);
    }
}
