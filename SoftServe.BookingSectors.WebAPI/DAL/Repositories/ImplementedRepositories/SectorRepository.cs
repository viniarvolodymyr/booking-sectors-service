using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementedRepositories
{
    public class SectorRepository : IBaseRepository<Sector>
    {
        private readonly BookingSectorContext db;
        private readonly DbSet<Sector> dbSet;

        public SectorRepository(BookingSectorContext context)
        {
            db = context;
            dbSet = db.Set<Sector>();
        }

        public async Task<IEnumerable<Sector>> GetAllEntitiesAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<Sector> GetEntityByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task InsertEntityAsync(Sector entityToInsert)
        {
            await dbSet.AddAsync(entityToInsert);
        }

        public void UpdateEntity(Sector entityToUpdate)
        {
            var x = dbSet.AsNoTracking().Where(e => e.Id == entityToUpdate.Id).FirstOrDefault();
            entityToUpdate.CreateUserId = x.CreateUserId;
            entityToUpdate.CreateDate = x.CreateDate;
            entityToUpdate.ModDate = DateTime.Now.AddDays(3);
            dbSet.Attach(entityToUpdate);
            db.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public void DeleteEntityByIdAsync(int id)
        {
            Sector sectorToDelete = dbSet.Find(id);
            dbSet.Remove(sectorToDelete);
        }
    }
}
