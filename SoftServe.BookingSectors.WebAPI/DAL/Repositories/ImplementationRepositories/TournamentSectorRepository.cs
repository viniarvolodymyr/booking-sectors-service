using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementationRepositories
{
    public class TournamentSectorRepository : IBaseRepository<TournamentSector>
    {
        private readonly BookingSectorContext db;
        private readonly DbSet<TournamentSector> dbSet;

        public TournamentSectorRepository(BookingSectorContext context)
        {
            db = context;
            dbSet = db.Set<TournamentSector>();
        }

        public Task<List<TournamentSector>> GetAllEntitiesAsync()
        {
            return dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TournamentSector> GetEntityByIdAsync(int id)
        {
            return await dbSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }
        public async ValueTask<EntityEntry<TournamentSector>> InsertEntityAsync(TournamentSector entity)
        {
           return  await dbSet.AddAsync(entity);
        }

        public void UpdateEntity(TournamentSector entity)
        {
            dbSet.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
        }
        public async Task DeleteEntityByIdAsync(int id)
        {
            TournamentSector existing = await dbSet.FindAsync(id);
            dbSet.Remove(existing);
        }


    }
}
