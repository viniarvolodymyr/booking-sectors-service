using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using Microsoft.EntityFrameworkCore;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

        public Task DeleteEntityByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Setting>> GetAllEntitiesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Setting> GetEntityAsync(int id)
        {
            return await dbSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Setting> GetEntityByIdAsync(int id)
        {
            return await dbSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public ValueTask<EntityEntry<Setting>> InsertEntityAsync(Setting entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(Setting entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }

        Task<List<Setting>> IBaseRepository<Setting>.GetAllEntitiesAsync()
        {
            throw new NotImplementedException();
        }
    }
}

