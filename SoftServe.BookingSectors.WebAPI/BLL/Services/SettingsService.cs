using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public SettingsService(IUnitOfWork uow, IMapper mapper)
        {
            _database = uow;
            _mapper = mapper;
        }
        enum settings
        {
            MAX_BOOKING_SECTORS = 1,
            MAX_BOOKING_DAYS = 2
        };
        public async Task<SettingsDTO> GetSettingByIdAsync(string name)
        {
            var entity = await _database.Settings.GetEntityByIdAsync((int)Enum.Parse(typeof(settings), name));
            if (entity == null)
            {
                return null;
            }
            var dto = _mapper.Map<Setting, SettingsDTO>(entity);
            return dto;
        }

        public async Task UpdateSettingsAsync(string name, SettingsDTO settingsDTO)
        {
            var entity = await _database.Settings.GetEntityByIdAsync((int)Enum.Parse(typeof(settings), name));
            var setting = _mapper.Map<SettingsDTO, Setting>(settingsDTO);
            setting.Id = (int)Enum.Parse(typeof(settings), name);
            setting.CreateDate = entity.CreateDate;
            setting.CreateUserId = entity.CreateUserId;
            setting.ModDate = DateTime.Now;
            _database.Settings.UpdateEntity(setting);
            await _database.SaveAsync();
        }
        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
