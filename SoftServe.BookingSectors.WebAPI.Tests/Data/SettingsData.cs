using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Mapping;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System.Collections.Generic;

namespace SoftServe.BookingSectors.WebAPI.Tests.Data
{
    public static class SettingsData
    {
        private static MapperConfiguration mapperConfiguration;
        private static IMapper mapper;
        static SettingsData()
        {
            mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<SettingProfile>();
            });
            mapper = mapperConfiguration.CreateMapper();
        }

        public static List<SettingsDTO> CreateSettingDTOs()
        {
            return mapper.Map<List<Setting>, List<SettingsDTO>>(CreateSettings());
        }

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
