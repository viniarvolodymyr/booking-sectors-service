using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces
{
    public interface IRegistrationService
    {
        Task<UserDTO> InsertUserAsync(UserDTO userDTO);
        Task<UserDTO> GetUserByEmailAsync(string email);
        Task<bool> ResetPasswordAsync(UserDTO userDTO);
        Task<UserDTO> SetNewPassword(UserDTO userDTO, string Hash);

    }
}
