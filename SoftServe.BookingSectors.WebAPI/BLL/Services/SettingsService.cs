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
            var entity = await _database.Settings.GetEntityAsync((int)Enum.Parse(typeof(settings), name));
            if (entity == null)
            {
                return null;
            }
            var dto = _mapper.Map<Setting, SettingsDTO>(entity);
            return dto;
        }

        public async Task UpdateSettingsAsync(string name1, string name2, int value1, int value2)
        {
            var entity1 = await _database.Settings.GetEntityAsync((int)Enum.Parse(typeof(settings), name1));
            var setting1 = _mapper.Map<Setting, SettingsDTO>(entity1);
            setting1.value = value1;
            
            var entity2 = await _database.Settings.GetEntityAsync((int)Enum.Parse(typeof(settings), name1));
            var setting2 = _mapper.Map<Setting, SettingsDTO>(entity2);
            setting2.value = value2;
            await _database.SaveAsync();
        }
        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
