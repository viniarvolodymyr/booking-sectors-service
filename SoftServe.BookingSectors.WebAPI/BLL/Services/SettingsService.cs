using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IUnitOfWork database;
        private readonly IMapper mapper;

        public SettingsService(IUnitOfWork database, IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }
        enum settings
        {
            MAX_BOOKING_SECTORS = 1,
            MAX_BOOKING_DAYS = 2
        };
        public async Task<SettingsDTO> GetSettingByIdAsync(string name)
        {
            var entity = await database.SettingRepository.GetEntityByIdAsync((int)Enum.Parse(typeof(settings), name));
            if (entity == null)
            {
                return null;
            }
            var dto = mapper.Map<Setting, SettingsDTO>(entity);
            return dto;
        }

        public async Task UpdateSettingsAsync(string name, SettingsDTO settingsDTO)
        {
            var entity = await database.SettingRepository.GetEntityByIdAsync((int)Enum.Parse(typeof(settings), name));
            var setting = mapper.Map<SettingsDTO, Setting>(settingsDTO);
            setting.Id = (int)Enum.Parse(typeof(settings), name);
            setting.CreateDate = entity.CreateDate;
            setting.CreateUserId = entity.CreateUserId;
            setting.ModDate = DateTime.Now;
            database.SettingRepository.UpdateEntity(setting);
            await database.SaveAsync();
        }
    }
}
