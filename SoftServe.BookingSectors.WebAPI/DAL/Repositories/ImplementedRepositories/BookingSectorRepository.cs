using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementedRepositories
{
    public class BookingSectorRepository : IBaseRepository<BookingSector>
    {
        private readonly BookingSectorContext db;
        private readonly DbSet<BookingSector> dbSet;

        public BookingSectorRepository(BookingSectorContext context)
        {
            db = context;
            dbSet = db.Set<BookingSector>();
        }

        public async Task DeleteEntityAsync(int id)
        {
            var existingBooking = await dbSet.FindAsync(id);
            dbSet.Remove(existingBooking);
        }

        public async Task<IEnumerable<BookingSector>> GetAllEntitiesAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<BookingSector> GetEntityAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task InsertEntityAsync(BookingSector entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task SaveEntityAsync()
        {
            await db.SaveChangesAsync();
        }

        public void UpdateEntity(BookingSector entity)
        {
            throw new NotImplementedException();
        }
    }
}
