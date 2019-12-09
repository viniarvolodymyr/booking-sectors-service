using SoftServe.BookingSectors.WebAPI.BLL.DTO;
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
        public void UpdateSettingsAsync(string name1, string name2, int value1, int value2);
        void Dispose();
    }
}
