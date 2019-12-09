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
        private readonly IUnitOfWork Database;
        private readonly IMapper _mapper;

        public SettingsService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }
        enum settings
        {
            MAX_BOOKING_SECTORS = 1,
            MAX_BOOKING_DAYS = 2
        };
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<SettingsDTO> GetSettingByIdAsync(string name)
        {
            var entity = await Database.Settings.GetEntityAsync((int)Enum.Parse(typeof(settings), name));
            if (entity == null)
            {
                return null;
            }
            var dto = _mapper.Map<Setting, SettingsDTO>(entity);
            return dto;
        }

        public async void UpdateSettingsAsync(string name1, string name2, int value1, int value2)
        {
            Setting setting1 = await Database.Settings.GetEntityAsync((int)Enum.Parse(typeof(settings), name1));
            setting1.Value = value1;
            Setting setting2 = await Database.Settings.GetEntityAsync((int)Enum.Parse(typeof(settings), name1));
            setting2.Value = value2;

        }
    }
}
