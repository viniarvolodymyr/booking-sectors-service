﻿using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces
{
    public interface ISettingsService
    {
        Task<IEnumerable<SettingsDTO>> GetSettingsAsync();
        Task<SettingsDTO> GetSettingByIdAsync(int id);
        Task<SettingsDTO> UpdateSettingsAsync(int id, SettingsDTO settingsDTO);
    }
}
