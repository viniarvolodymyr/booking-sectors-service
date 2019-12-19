using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.Interfaces
{
    public interface ISettingsService
    {
        Task<IEnumerable<SettingsDTO>> GetSettingsAsync();
        Task<SettingsDTO> GetSettingByIdAsync(int id);
        public Task<Setting> UpdateSettingsAsync(int id, SettingsDTO settingsDTO);
    }
}
