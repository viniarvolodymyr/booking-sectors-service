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

        public void Create(Tournament book)
        {
            db.Tournament.Add(book);
        }

        public void Update(Tournament book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Tournament book = db.Tournament.Find(id);
            if (book != null)
                db.Tournament.Remove(book);
        }

    }
}
