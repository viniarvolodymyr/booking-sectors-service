using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementedRepositories;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories.Interfaces;

namespace SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly BookingSectorContext db;
        private SectorRepository sectorRepository;
        private SettingsRepository settingsRepository;
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

        public IBaseRepository<Setting> Settings
        {
            get
            {
                if (settingsRepository == null)
                    settingsRepository = new SettingsRepository(db);
                return settingsRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
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
                return false;
            }
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
