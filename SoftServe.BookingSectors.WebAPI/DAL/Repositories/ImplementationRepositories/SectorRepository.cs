using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementationRepositories

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

        public async Task InsertEntityAsync(Sector entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void UpdateEntity(Sector entity)
        {
            dbSet.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
        }
        public async Task DeleteEntityAsync(int id)
        {
            Sector existing = await dbSet.FindAsync(id);
            dbSet.Remove(existing);
        }
    }
}
