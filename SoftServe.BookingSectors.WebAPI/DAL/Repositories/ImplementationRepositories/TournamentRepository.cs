using Microsoft.EntityFrameworkCore;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.BLL.ErrorHandling;
using System.Net;

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
            var result = tournamentSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
            if (result.Result == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Tournament with id: {id} not found when trying to get tournament.");
            }
            return result;
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
            Tournament entityToDelete = await tournamentSet.FindAsync(id);
            if (entityToDelete == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Tournament with id: {id} not found when trying to delete tournament. Tournament wasn't deleted.");
            }
            return tournamentSet.Remove(entityToDelete).Entity;
        }
    }
}

