using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementedRepositories
{
    public class TournamentSectorRepository : IBaseRepository<TournamentSector>
    {
        private readonly BookingSectorContext db;
        private readonly DbSet<TournamentSector> dbSet;

        public TournamentSectorRepository(BookingSectorContext context)
        {
            db = context;
            dbSet = db.Set<TournamentSector>();
        }

        public async Task<IEnumerable<TournamentSector>> GetAllEntitiesAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TournamentSector> GetEntityAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Create(TournamentSector tournamentSector)
        {
            db.TournamentSector.Add(tournamentSector);
        }

        public void Update(TournamentSector tournamentSector)
        {
            db.Entry(tournamentSector).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            TournamentSector tournamentSector = db.TournamentSector.Find(id);
            if (tournamentSector != null)
                db.TournamentSector.Remove(tournamentSector);
        }
    }
}
