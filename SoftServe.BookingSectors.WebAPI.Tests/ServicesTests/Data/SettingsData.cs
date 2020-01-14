using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System.Collections.Generic;

namespace SoftServe.BookingSectors.WebAPI.Tests.ServicesTests.Data
{
    public static class SettingsData
    {
        public static List<Setting> CreateSettings()
        {
            return new List<Setting>()
            {
                new Setting
                {
                    Id = 1,
                    Name = "Max Booking Days",
                    Value = 4,
                    ModUserId = 1
                },
                new Setting
                {
                    Id = 2,
                    Name = "Max Booking Settings",
                    Value = 3,
                    ModUserId = 1
                }
            };
        }
        public static SettingsDTO CreateSettingDTO()
        {
            return new SettingsDTO
                {
                    Id = 1,
                    Name = "Max Booking Days",
                    Value = 2,
                    ModUserId = 2
                };
        }
    }
}
