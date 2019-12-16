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
        private readonly BookingSectorContext context;
        private readonly DbSet<TournamentSector> tournamentSectorSet;

        public TournamentSectorRepository(BookingSectorContext context)
        {
            this.context = context;
            tournamentSectorSet = context.Set<TournamentSector>();
        }

        public Task<List<TournamentSector>> GetAllEntitiesAsync()
        {
            return tournamentSectorSet.AsNoTracking().ToListAsync();
        }

        public Task<TournamentSector> GetEntityByIdAsync(int id)
        {
            return tournamentSectorSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public ValueTask<EntityEntry<TournamentSector>> InsertEntityAsync(TournamentSector entityToInsert)
        {
            return tournamentSectorSet.AddAsync(entityToInsert);
        }

        public void UpdateEntity(TournamentSector entityToUpdate)
        {
            tournamentSectorSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async Task<EntityEntry<TournamentSector>> DeleteEntityByIdAsync(int id)
        {
            TournamentSector entityToDelete = await tournamentSectorSet.FindAsync(id);
            return tournamentSectorSet.Remove(entityToDelete);
        }
    }
}
