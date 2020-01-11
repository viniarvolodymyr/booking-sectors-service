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
    public class TournamentRepository: IBaseRepository<Tournament>
    {
        private readonly BookingSectorContext context;
        private readonly DbSet<Tournament> tournamentSet;

        public TournamentRepository(BookingSectorContext context)
        {
            this.context = context;
            tournamentSet = context.Set<Tournament>();
        }

        public  Task<List<Tournament>> GetAllEntitiesAsync()
        {
            return tournamentSet.AsNoTracking().ToListAsync();
        }

        public async Task<Tournament>  GetEntityByIdAsync(int id)
        {
            return await tournamentSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public IQueryable<Tournament> GetByCondition(Expression<Func<Tournament, bool>> expression)
        {
            return tournamentSet.Where(expression).AsNoTracking();
        }

        public async Task<Tournament> InsertEntityAsync(Tournament entityToInsert)
        {
            return (await tournamentSet.AddAsync(entityToInsert)).Entity;
        }

        public Tournament UpdateEntity(Tournament entityToUpdate)
        {
            return tournamentSet.Update(entityToUpdate).Entity;
        }

        public async Task<Tournament> DeleteEntityByIdAsync(int id)
        {
            Tournament tournamentToDelete = await tournamentSet.FindAsync(id);
            return tournamentSet.Remove(tournamentToDelete).Entity;
        }

    }
}
