using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementedRepositories
{
    public class SettingRepository : IBaseRepository<Setting>
    {
        private readonly BookingSectorContext context;
        private readonly DbSet<Setting> settingSet;
        public SettingRepository(BookingSectorContext context)
        {
            this.context = context;
            settingSet = context.Set<Setting>();
        }
        public Task<List<Setting>> GetAllEntitiesAsync()
        {
            return settingSet.AsNoTracking().ToListAsync();
        }
        public Task<Setting> GetEntityByIdAsync(int id)
        {
            return settingSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public IQueryable<Setting> GetByCondition(Expression<Func<Setting, bool>> expression)
        {
            return settingSet.Where(expression).AsNoTracking();
        }

        public ValueTask<EntityEntry<Setting>> InsertEntityAsync(Setting entityToInsert)
        {
            return settingSet.AddAsync(entityToInsert);
        }
        public void UpdateEntity(Setting entityToUpdate)
        {
            settingSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public async Task<EntityEntry<Setting>> DeleteEntityByIdAsync(int id)
        {
            Setting sectorToDelete = await settingSet.FindAsync(id);
            return settingSet.Remove(sectorToDelete);
        }
    }
}

