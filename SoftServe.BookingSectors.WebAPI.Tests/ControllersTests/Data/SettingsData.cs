using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using System.Collections.Generic;

namespace SoftServe.BookingSectors.WebAPI.Tests.ControllersTests.Data
{
    class SettingsData
    {
        public SettingsData() { }
        public List<SettingsDTO> SettingsDTO { get; } = new List<SettingsDTO>()
        {
            new SettingsDTO
            {
                Id = 1,
                Name = "Max Booking Days",
                Value = 4,
                ModUserId = 1
            },
            new SettingsDTO
            {
                Id = 2,
                Name = "Max Booking Settings",
                Value = 3,
                ModUserId = 1
            }
        };
        public SettingsDTO settingToInsert = new SettingsDTO()
        {
            Id = 1,
            Name = "Max Booking Days",
            Value = 2,
            ModUserId = 2
        };


    }
}
