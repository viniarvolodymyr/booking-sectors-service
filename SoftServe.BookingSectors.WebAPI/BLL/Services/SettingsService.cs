using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
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
        public async Task<IEnumerable<SettingsDTO>> GetSettingsAsync()
        {
            var settings = await database.SettingRepository.GetAllEntitiesAsync();
            var dtos = mapper.Map<IEnumerable<Setting>, IEnumerable<SettingsDTO>>(settings);

            return dtos;
        }
        public async Task<SettingsDTO> GetSettingByIdAsync(int id)
        {
            var entity = await database.SettingRepository.GetEntityByIdAsync(id);
            if (entity == null)
            {
                return null;
            }
            var dto = mapper.Map<Setting, SettingsDTO>(entity);
            return dto;
        }

        public async Task<SettingsDTO> UpdateSettingsAsync(int id, SettingsDTO settingsDTO)
        {
            var entity = await database.SettingRepository.GetEntityByIdAsync(id);
            var setting = mapper.Map<SettingsDTO, Setting>(settingsDTO);
            setting.Id = id;
            setting.CreateDate = entity.CreateDate;
            setting.CreateUserId = entity.CreateUserId;
            setting.ModDate = DateTime.Now;
            database.SettingRepository.UpdateEntity(setting);
            bool isSaved = await database.SaveAsync();

            return (isSaved == true) ? settingsDTO : null;
        }
    }
}
