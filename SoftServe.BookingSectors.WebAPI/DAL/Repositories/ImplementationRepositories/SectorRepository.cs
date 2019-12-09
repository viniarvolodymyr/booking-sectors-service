using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public Task<List<Sector>> GetAllEntitiesAsync()
        {
            return dbSet.AsNoTracking().ToListAsync();
        }

        public  Task<Sector> GetEntityByIdAsync(int id)
        {
             return dbSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public  ValueTask<EntityEntry<Sector>> InsertEntityAsync(Sector entityToInsert)
        {
             return dbSet.AddAsync(entityToInsert);
        }

        public void UpdateEntity(Sector entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public async Task DeleteEntityByIdAsync(int id)
        {
            Sector sectorToDelete =  await dbSet.FindAsync(id);
            dbSet.Remove(sectorToDelete);
        }
    }
}
