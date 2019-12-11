using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementationRepositories
{
    public class TournamentRepository : IBaseRepository<Tournament>
    {
        private readonly BookingSectorContext context;
        private readonly DbSet<Tournament> tournamentSet;

        public TournamentRepository(BookingSectorContext context)
        {
            this.context = context;
            tournamentSet = context.Set<Tournament>();
        }

        public Task<List<Tournament>> GetAllEntitiesAsync()
        {
            return tournamentSet.AsNoTracking().ToListAsync();
        }

        public Task<Tournament> GetEntityByIdAsync(int id)
        {
            return tournamentSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }
        public ValueTask<EntityEntry<Tournament>> InsertEntityAsync(Tournament entityToInsert)
        {
            return tournamentSet.AddAsync(entityToInsert);
        }

        public void UpdateEntity(Tournament entityToUpdate)
        {
            tournamentSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async Task DeleteEntityByIdAsync(int id)
        {
            Tournament entityToDelete = await tournamentSet.FindAsync(id);
            tournamentSet.Remove(entityToDelete);
        }
    }
}
