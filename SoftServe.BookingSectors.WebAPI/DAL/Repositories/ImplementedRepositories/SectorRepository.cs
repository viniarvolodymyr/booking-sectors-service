using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementedRepositories
{
    public class SectorRepository : IBaseRepository<Sector>
    {
        private readonly BookingSectorContext db;
        private readonly DbSet<Sector> dbSet;

        public SectorRepository(BookingSectorContext context)
        {
            db = context;
            dbSet = db.Set<Sector>();
        }

        public async Task<IEnumerable<Sector>> GetAllEntitiesAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<Sector> GetEntityAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Create(Sector book)
        {
            db.Sector.Add(book);
        }

        public void Update(Sector book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Sector book = db.Sector.Find(id);
            if (book != null)
                db.Sector.Remove(book);
        }
    }
}
