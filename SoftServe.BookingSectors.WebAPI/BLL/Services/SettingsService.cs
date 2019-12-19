using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories.Interfaces;
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

        public SettingsService(IUnitOfWork uow, IMapper mapper)
        {
            database = uow;
            mapper = mapper;
        }
        public async Task<SettingsDTO> GetSettingByIdAsync(int id)
        {
            var entity = await database.SettingsRepository.GetEntityByIdAsync(id);
            if (entity == null)
            {
                return null;
            }
            var dto = mapper.Map<Setting, SettingsDTO>(entity);
            return dto;
        }
        public async Task<IEnumerable<SettingsDTO>> GetSettingsAsync()
        {
            var settings = await database.SettingsRepository.GetAllEntitiesAsync();
            var dtos = mapper.Map<IEnumerable<Setting>, IEnumerable<SettingsDTO>>(settings);

            return dtos;
        }

        public async Task<Setting> UpdateSettingsAsync(int id, SettingsDTO settingsDTO)
        {
            var existingSetting = await database.SettingsRepository.GetEntityByIdAsync(id);
            if (existingSetting == null)
            {
                return null;
            }
            var setting = mapper.Map<SettingsDTO, Setting>(settingsDTO);
            setting.Id = id;
            setting.CreateUserId = existingSetting.CreateUserId;
            setting.CreateDate = existingSetting.CreateDate;
            setting.ModDate = DateTime.Now;
            database.SettingsRepository.UpdateEntity(setting);
            bool isSaved = await database.SaveAsync();

            return (isSaved == true) ? setting : null;
        }
    }
}
