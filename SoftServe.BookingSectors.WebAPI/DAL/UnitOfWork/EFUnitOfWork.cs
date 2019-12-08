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
        private SectorRepository sectorRepository;
        private UserRepository userRepository;
        private TournamentSectorRepository tournamentSectorRepository;
        private TournamentRepository tournamentRepository;
        private bool disposed = false;
        public EFUnitOfWork(BookingSectorContext context)
        {
            db = context;
        }
        public IBaseRepository<Sector> Sectors
        {
            get
            {
                if (sectorRepository == null)
                    sectorRepository = new SectorRepository(db);
                return sectorRepository;
            }
        }
        public IBaseRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IBaseRepository<TournamentSector> TournamentSectors
        {
            get
            {
                if (tournamentSectorRepository == null)
                    tournamentSectorRepository = new TournamentSectorRepository(db);
                return tournamentSectorRepository;
            }
        }
        public IBaseRepository<Tournament> Tournaments
        {
            get
            {
                if (tournamentRepository == null)
                    tournamentRepository = new TournamentRepository(db);
                return tournamentRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges();
            get { return sectorRepository ??= new SectorRepository(db); }
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
