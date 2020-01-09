using SoftServe.BookingSectors.WebAPI.DAL.Models;
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
        Task<User> UpdateUserById(int id, UserDTO userDTO);
        Task<User> UpdateUserPassById(int id, UserDTO userDTO);
        Task<User> DeleteUserByIdAsync(int id);
        Task<bool> CheckPasswords(string password, int id);
        Task<User> UpdateUserPhotoById(int id, IFormFile image);
        Task<IFormFile> GetUserPhotoById(int id);
        Task<RegistrationDTO> SendEmailAsync(string email);
    }
}
