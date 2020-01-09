using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces
{
    public interface IRegistrationService
    {
        Task<RegistrationDTO> InsertUserAsync(RegistrationDTO userDTO);
    }
}
