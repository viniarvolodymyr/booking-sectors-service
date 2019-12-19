using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using Microsoft.EntityFrameworkCore;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories.Interfaces;
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

        public async Task DeleteEntityByIdAsync(int id)
        {
            Setting settingToDelete = await dbSet.FindAsync(id);
            dbSet.Remove(settingToDelete);
        }
        public async Task<List<Setting>> GetAllEntitiesAsync()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }
        public async Task<Setting> GetEntityByIdAsync(int id)
        {
            return await dbSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }
        public ValueTask<EntityEntry<Setting>> InsertEntityAsync(Setting entityToInsert)
        {
            return dbSet.AddAsync(entityToInsert);
        }
        public void UpdateEntity(Setting entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}

