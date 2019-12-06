using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;

namespace SoftServe.BookingSectors.WebAPI.BLL.Interfaces
{
    interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllSectorsAsync();
        Task<UserDTO> GetSectorByIdAsync(int id);
        void Dispose();
    }
}
