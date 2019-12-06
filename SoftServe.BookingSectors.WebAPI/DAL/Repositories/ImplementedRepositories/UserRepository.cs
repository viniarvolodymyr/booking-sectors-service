using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Create(User user)
        {
            db.User.Add(user);
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User user = db.User.Find(id);
            if (user != null)
                db.User.Remove(user);
        }
    }
}
