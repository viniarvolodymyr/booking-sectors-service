using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementedRepositories
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

        public async Task<IEnumerable<User>> GetAllEntitiesAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<User> GetEntityAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task InsertEntityAsync(User entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void UpdateEntity(User entity)
        {
            dbSet.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
        }
        public async Task DeleteEntityAsync(int id)
        {
            User existing = await dbSet.FindAsync(id);
            dbSet.Remove(existing);
        }
        public async Task SaveEntityAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
