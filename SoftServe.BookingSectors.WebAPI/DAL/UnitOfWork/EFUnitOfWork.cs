using System;
<<<<<<< HEAD
=======
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
>>>>>>> master
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementationRepositories;

namespace SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
<<<<<<< HEAD
        private readonly BookingSectorContext db;
        private SectorRepository sectorRepository;
        private BookingSectorRepository bookingRepository;
=======
        private readonly BookingSectorContext context;
        private SectorRepository sectorsRepository;
        private bool disposed = false;
>>>>>>> master
        public EFUnitOfWork(BookingSectorContext context)
        {
            this.context = context;
        }
        public IBaseRepository<Sector> SectorsRepository
        {
            get { return sectorsRepository ??= new SectorRepository(context); }
        }
<<<<<<< HEAD

        public IBaseRepository<BookingSector> BookingSectors
        {
            get
            {
                if (bookingRepository == null)
                    bookingRepository = new BookingSectorRepository(db);
                return bookingRepository;
            }
        }

        public void Save()
=======
        public async Task<bool> SaveAsync()
>>>>>>> master
        {
            try
            {
                var changes = context.ChangeTracker.Entries().Count(
                    p => p.State == EntityState.Modified || p.State == EntityState.Deleted
                                                         || p.State == EntityState.Added);
                if (changes == 0) return true;

                return await context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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
