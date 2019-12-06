using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementedRepositories
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

        public async Task<IEnumerable<TournamentSector>> GetAllEntitiesAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TournamentSector> GetEntityAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task InsertEntityAsync(TournamentSector entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void UpdateEntity(TournamentSector entity)
        {
            dbSet.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
        }
        public async Task DeleteEntityAsync(int id)
        {
            TournamentSector existing = await dbSet.FindAsync(id);
            dbSet.Remove(existing);
        }
        public async Task SaveEntityAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
