using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementationRepositories;

namespace SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly BookingSectorContext db;
       
        private TournamentSectorRepository tournamentSectorsRepository;
        private TournamentRepository tournamentRepository;
        private bool disposed = false;
        public EFUnitOfWork(BookingSectorContext context)
        {
            db = context;
        }
     
        public IBaseRepository<Tournament> tournamentRepositoty
        {
            get { return tournamentRepository ??= new TournamentRepository(db); }
        }
        public IBaseRepository<TournamentSector> TournamentSectors
        {
            get { return tournamentSectorsRepository ??= new TournamentSectorRepository(db); }
        }
        public async Task<bool> SaveAsync()
        {
            try
            {
                var changes = db.ChangeTracker.Entries().Count(
                    p => p.State == EntityState.Modified || p.State == EntityState.Deleted
                                                         || p.State == EntityState.Added);
                if (changes == 0) return true;

                return await db.SaveChangesAsync() > 0;
            }
            catch
            {
                // Logger = ex.Message
                return false;
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
