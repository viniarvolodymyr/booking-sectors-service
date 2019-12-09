using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;

namespace SoftServe.BookingSectors.WebAPI.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int id);

        Task<UserDTO> GetUserByPhoneAsync(string phone);

        Task UpdateUserById(int id, UserDTO userDTO);

        void Dispose();
    }
}
