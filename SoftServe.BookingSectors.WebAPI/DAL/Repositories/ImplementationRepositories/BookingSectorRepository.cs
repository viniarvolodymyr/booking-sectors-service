using Microsoft.EntityFrameworkCore;
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

        public IQueryable<BookingSector> GetByCondition(Expression<Func<BookingSector, bool>> expression)
        {
            return bookingSectorSet.Where(expression).AsNoTracking();
        }

        public async Task<BookingSector> InsertEntityAsync(BookingSector entityToInsert)
        {
            return (await bookingSectorSet.AddAsync(entityToInsert)).Entity;
        }

        public void UpdateEntity(BookingSector entityToUpdate)
        {
            bookingSectorSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async Task<EntityEntry<BookingSector>> DeleteEntityByIdAsync(int id)
        {
            BookingSector entityToDelete = await bookingSectorSet.FindAsync(id);
            return bookingSectorSet.Remove(entityToDelete);
        }
    }
}