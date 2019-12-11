using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementationRepositories
{
    public class BookingSectorRepository : IBaseRepository<BookingSector>
    {
        private readonly BookingSectorContext context;
        private readonly DbSet<BookingSector> bookingSectorSet;

        public BookingSectorRepository(BookingSectorContext context)
        {
            this.context = context;
            bookingSectorSet = context.Set<BookingSector>();
        }

        public Task<List<BookingSector>> GetAllEntitiesAsync()
        {
            return bookingSectorSet.AsNoTracking().ToListAsync();
        }

        public Task<BookingSector> GetEntityByIdAsync(int id)
        {
            return bookingSectorSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public ValueTask<EntityEntry<BookingSector>> InsertEntityAsync(BookingSector entity)
        {
            return bookingSectorSet.AddAsync(entity);
        }

        public void UpdateEntity(BookingSector entity)
        {
            bookingSectorSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteEntityByIdAsync(int id)
        {
            BookingSector entityToDelete = await bookingSectorSet.FindAsync(id);
            bookingSectorSet.Remove(entityToDelete);
        }
    }
}
