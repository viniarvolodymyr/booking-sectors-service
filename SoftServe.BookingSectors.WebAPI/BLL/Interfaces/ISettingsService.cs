﻿using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.Interfaces
{
    public interface ISettingsService
    {
        Task<SettingsDTO> GetSettingByIdAsync(string name);
        public Task UpdateSettingsAsync(string name1, SettingsDTO settingsDTO);
        void Dispose();
    }
}