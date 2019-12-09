using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementationRepositories
{
    public class AuthenticationRepository: IBaseRepository<User>
    {
        private readonly BookingSectorContext context;
        private readonly DbSet<User> dbSet;

        public AuthenticationRepository(BookingSectorContext context)
        {
            this.context = context;
            dbSet = context.Set<User>();
        }

        public Task<List<User>> GetAllEntitiesAsync()
        {
            return dbSet.AsNoTracking().ToListAsync();
        }

        public Task<User> GetEntityByIdAsync(int id)
        {
            return dbSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public ValueTask<EntityEntry<User>> InsertEntityAsync(User entityToInsert)
        {
            return dbSet.AddAsync(entityToInsert);
        }

        public void UpdateEntity(User entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public async Task DeleteEntityByIdAsync(int id)
        {
            User userToDelete = await dbSet.FindAsync(id);
            dbSet.Remove(userToDelete);
        }


    }
}
