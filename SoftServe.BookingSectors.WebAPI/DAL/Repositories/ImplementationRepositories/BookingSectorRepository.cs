using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public Task<List<BookingSector>> GetAllEntitiesAsync()
        {
            return dbSet.AsNoTracking().ToListAsync();
        }
        public Task<BookingSector> GetEntityByIdAsync(int id)
        {
            return dbSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }
        public ValueTask<EntityEntry<BookingSector>> InsertEntityAsync(BookingSector entity)
        {
            return dbSet.AddAsync(entity);
        }
        public void UpdateEntity(BookingSector entity)
        {
            dbSet.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteEntityByIdAsync(int id)
        {
            var existingBooking = await dbSet.FindAsync(id);
            dbSet.Remove(existingBooking);
        }      
    }
}
