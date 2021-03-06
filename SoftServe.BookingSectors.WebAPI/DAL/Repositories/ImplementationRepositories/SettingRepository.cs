﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
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

        public async Task<Setting> InsertEntityAsync(Setting entityToInsert)
        {
            return (await settingSet.AddAsync(entityToInsert)).Entity;
        }
        public Setting UpdateEntity(Setting entityToUpdate)
        {
            return settingSet.Update(entityToUpdate).Entity;
        }
        public async Task<Setting> DeleteEntityByIdAsync(int id)
        {
            Setting sectorToDelete = await settingSet.FindAsync(id);
            return settingSet.Remove(sectorToDelete).Entity;
        }
    }
}

