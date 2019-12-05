using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Interfaces;
namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly BookingSectorContext db;
        private SectorRepository sectorRepository;
        public EFUnitOfWork(BookingSectorContext context)
        {
            db = context;
        }
        public IRepository<Sector> Sectors
        {
            get
            {
                if (sectorRepository == null)
                    sectorRepository = new SectorRepository(db);
                return sectorRepository;
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
