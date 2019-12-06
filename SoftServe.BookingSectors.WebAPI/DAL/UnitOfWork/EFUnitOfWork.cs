using System;
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

        public IBaseRepository<User> User
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public Task<int> SaveAsync()
        {
            return db.SaveChangesAsync();
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
