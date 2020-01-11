using Microsoft.EntityFrameworkCore;
using SoftServe.BookingSectors.WebAPI.BLL.ErrorHandling;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementationRepositories
{
    public class SectorRepository : IBaseRepository<Sector>
    {
        private readonly BookingSectorContext context;
        private readonly DbSet<Sector> sectorSet;

        public SectorRepository(BookingSectorContext context)
        {
            this.context = context;
            sectorSet = context.Set<Sector>();
        }

        public Task<List<Sector>> GetAllEntitiesAsync()
        {
            return sectorSet.AsNoTracking().ToListAsync();
        }

        public Task<Sector> GetEntityByIdAsync(int id)
        {
            var result = sectorSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
            if (result.Result == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Sector with id: {id} not found when trying to get sector.");
            }

            return result;
        }

        public IQueryable<Sector> GetByCondition(Expression<Func<Sector, bool>> expression)
        {
            return sectorSet.Where(expression).AsNoTracking().AsQueryable();
        }

        public async Task<Sector> InsertEntityAsync(Sector entityToInsert)
        {
            return (await sectorSet.AddAsync(entityToInsert)).Entity;
        }

        public void UpdateEntity(Sector entityToUpdate)
        {
            sectorSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async Task<Sector> DeleteEntityByIdAsync(int id)
        {
            var entityToDelete = await sectorSet.FindAsync(id);
            if (entityToDelete == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Sector with id: {id} not found when trying to delete sector. Sector wasn't deleted.");
            }

            return sectorSet.Remove(entityToDelete).Entity;
        }
    }
}
