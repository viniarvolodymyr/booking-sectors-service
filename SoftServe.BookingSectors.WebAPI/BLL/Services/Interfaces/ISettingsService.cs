using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces
{
    public interface ISettingsService
    {
        Task<SettingsDTO> GetSettingByIdAsync(string name);
        public Task UpdateSettingsAsync(string name1, SettingsDTO settingsDTO);
    }
}
