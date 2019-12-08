using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

        public async Task<List<TournamentSector>> GetAllEntitiesAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TournamentSector> GetEntityByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async ValueTask<EntityEntry<TournamentSector>> InsertEntityAsync(TournamentSector entity)
        {
           return await dbSet.AddAsync(entity);
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
