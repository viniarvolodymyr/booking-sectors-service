using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementationRepositories
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
            throw new NotImplementedException();
        }

        public Task<Setting> GetEntityByIdAsync(int id)
        {
            return settingSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public ValueTask<EntityEntry<Setting>> InsertEntityAsync(Setting entityToInsert)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(Setting entityToUpdate)
        {
            settingSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public Task<EntityEntry<Setting>> DeleteEntityByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

