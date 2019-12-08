using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementationRepositories
{
    public class SectorRepository : IBaseRepository<Sector>
    {
        private readonly BookingSectorContext context;
        private readonly DbSet<Sector> dbSet;

        public SectorRepository(BookingSectorContext context)
        {
            this.context = context;
            dbSet = context.Set<Sector>();
        }

        public async Task<IEnumerable<Sector>> GetAllEntitiesAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<Sector> GetEntityByIdAsync(int id)
        {
            return await dbSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertEntityAsync(Sector entityToInsert)
        {
            await dbSet.AddAsync(entityToInsert);
        }

        public void UpdateEntity(Sector entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public void DeleteEntityByIdAsync(int id)
        {
            Sector sectorToDelete = dbSet.Find(id);
            dbSet.Remove(sectorToDelete);
        }
    }
}
