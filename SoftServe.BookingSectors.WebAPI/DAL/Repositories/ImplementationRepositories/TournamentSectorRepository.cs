using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
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

        public async Task<TournamentSector> GetEntityByIdAsync(int id)
        {
            return await tournamentSectorSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public IQueryable<TournamentSector> GetByCondition(Expression<Func<TournamentSector, bool>> expression)
        {
            return tournamentSectorSet.Where(expression).AsNoTracking();
        }

        public async ValueTask<EntityEntry<TournamentSector>> InsertEntityAsync(TournamentSector entity)
        {
           return  await tournamentSectorSet.AddAsync(entity);
        }

        public void UpdateEntity(TournamentSector entity)
        {
            tournamentSectorSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
        public async Task<EntityEntry<TournamentSector>> DeleteEntityByIdAsync(int id)
        {
            TournamentSector existing = await tournamentSectorSet.FindAsync(id);
            return tournamentSectorSet.Remove(existing);
        }


    }
}
