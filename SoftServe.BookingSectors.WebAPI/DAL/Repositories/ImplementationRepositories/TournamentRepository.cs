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
    public class TournamentRepository: IBaseRepository<Tournament>
    {
        private readonly BookingSectorContext db;
        private readonly DbSet<Tournament> dbSet;

        public TournamentRepository(BookingSectorContext context)
        {
            db = context;
            dbSet = db.Set<Tournament>();
        }

        public  Task<List<Tournament>> GetAllEntitiesAsync()
        {
            return  dbSet.ToListAsync();
        }

        public async Task<Tournament>  GetEntityByIdAsync(int id)
        {
            return await dbSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }
        public  ValueTask<EntityEntry<Tournament>> InsertEntityAsync(Tournament entity)
        {
            return  dbSet.AddAsync(entity);
          
        }

        public void UpdateEntity(Tournament entity)
        {
            dbSet.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteEntityByIdAsync(int id)
        {
            Tournament existing = await dbSet.FindAsync(id);
            dbSet.Remove(existing);
        }

    }
}
