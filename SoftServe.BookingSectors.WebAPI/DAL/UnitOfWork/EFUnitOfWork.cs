using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementedRepositories;

namespace SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly BookingSectorContext db;
        private SectorRepository sectorRepository;
        private UserRepository userRepository;
        private TournamentSectorRepository tournamentSectorRepository;
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

        public void Save()
        {
            db.SaveChanges();
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
