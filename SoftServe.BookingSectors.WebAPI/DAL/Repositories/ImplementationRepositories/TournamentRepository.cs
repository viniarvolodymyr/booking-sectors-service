using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementedRepositories
{
    public class TournamentRepository: IBaseRepository<Tournament>
    {
        private readonly BookingSectorContext db;
        private readonly DbSet<Tournament> dbSet;

        public TournamentRepository(BookingSectorContext context)
        {
            db = context;
            dbSet = db.Set<Tournament>();
        }

        public async Task<IEnumerable<Tournament>> GetAllEntitiesAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<Tournament> GetEntityAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }
        public async Task InsertEntityAsync(Tournament entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void UpdateEntity(Tournament entity)
        {
            dbSet.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
        }
        public async Task DeleteEntityAsync(int id)
        {
            Tournament existing = await dbSet.FindAsync(id);
            dbSet.Remove(existing);
        }
        public async Task SaveEntityAsync()
        {
            await db.SaveChangesAsync();
        }

    }
}
