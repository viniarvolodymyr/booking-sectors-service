using System;
using System.Runtime.Intrinsics.X86;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SoftServe.BookingSectors.WebAPI.BLL.ErrorHandling;
using System.Net;


namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementationRepositories
{
    public class UserRepository : IBaseRepository<User>
    {

        private readonly BookingSectorContext db;
        private readonly DbSet<User> dbSet;

        public UserRepository(BookingSectorContext context)
        {
            db = context;
            dbSet = db.Set<User>();
        }
        public Task<List<User>> GetAllEntitiesAsync()
        {
            return dbSet.Include(x => x.Role).AsNoTracking().ToListAsync();
        }
        public Task<User> GetEntityByIdAsync(int id)
        {

            var result = dbSet.Include(x => x.Role).AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
            if (result.Result == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"User with id: {id} not found when trying to get entity.");
            }

            return result;

        }

        public async ValueTask<EntityEntry<User>> InsertEntityAsync(User entityToInsert)
        {
            return await dbSet.AddAsync(entityToInsert);
        }

        public void UpdateEntity(User entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            db.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public async Task DeleteEntityByIdAsync(int id)
        {
            var userToDelete = await dbSet.FindAsync(id);
            if (userToDelete == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"User with id: {id} not found when trying to update entity. Entity was no Deleted.");
            }
            dbSet.Remove(userToDelete);
        }
    }
}
