using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using Microsoft.EntityFrameworkCore;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories.Interfaces;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementedRepositories
{
    public class SettingsRepository : IBaseRepository<Setting>
    {
        private readonly BookingSectorContext db;
        private readonly DbSet<Setting> dbSet;

        public SettingsRepository(BookingSectorContext context)
        {
            db = context;
            dbSet = db.Set<Setting>();
        }
        enum settings
        {
            MAX_BOOKING_SECTORS = 1,
            MAX_BOOKING_DAYS = 2
        };
        public void UpdateSettings(string name1, string name2, int value1, int value2)
        {
            Setting maxBookingDays = dbSet.Find((int)Enum.Parse(typeof(settings), name1));
            maxBookingDays.Value = value1;
            Setting maxBookingSectors = dbSet.Find((int)Enum.Parse(typeof(settings), name2));
            maxBookingSectors.Value = value2;
        }

        public Task<IEnumerable<Setting>> GetAllEntitiesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Setting> GetEntityAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }
        public void UpdateEntity(Setting entity)
        {
            throw new NotImplementedException();
        }
    }
}

