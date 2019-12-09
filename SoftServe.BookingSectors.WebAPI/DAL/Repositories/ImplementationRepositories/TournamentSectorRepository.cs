using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementationRepositories
{
    public class TournamentSectorRepository : IBaseRepository<TournamentSector>
    {
        private readonly BookingSectorContext context;
        private readonly DbSet<TournamentSector> dbSet;

        public TournamentSectorRepository(BookingSectorContext context)
        {
            this.context = context;
            dbSet = context.Set<TournamentSector>();
        }

        public Task<List<TournamentSector>> GetAllEntitiesAsync()
        {
            return dbSet.AsNoTracking().ToListAsync();
        }

        public Task<TournamentSector> GetEntityByIdAsync(int id)
        {
            return dbSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public ValueTask<EntityEntry<TournamentSector>> InsertEntityAsync(TournamentSector entityToInsert)
        {
            return dbSet.AddAsync(entityToInsert);
        }

        public void UpdateEntity(TournamentSector entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public async Task DeleteEntityByIdAsync(int id)
        {
            TournamentSector sectorToDelete = await dbSet.FindAsync(id);
            dbSet.Remove(sectorToDelete);
        }

    }
}
