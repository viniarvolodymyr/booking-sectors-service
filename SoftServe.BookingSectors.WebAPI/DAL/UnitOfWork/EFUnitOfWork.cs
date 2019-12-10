using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementationRepositories;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementedRepositories;

namespace SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly BookingSectorContext context;
        private SettingsRepository settingsRepository;
        private SectorRepository sectorsRepository;
        private UserRepository usersRepository;
        private TournamentSectorRepository tournamentSectorsRepository;
        private TournamentRepository tournamentRepository;
        private BookingSectorRepository bookingRepository;
        public EFUnitOfWork(BookingSectorContext context)
        {
            this.context = context;
        }

        public IBaseRepository<TournamentSector> TournamentSectorsRepository
        {
            get { return tournamentSectorsRepository ??= new TournamentSectorRepository(context); }
        }
        public IBaseRepository<Setting> Settings
        {
            get
            {
                if (settingsRepository == null)
                    settingsRepository = new SettingsRepository(context);
                return settingsRepository;
            }
        }

        public IBaseRepository<User> UsersRepository
        {
            get { return usersRepository ??= new UserRepository(context); }
        }

        public IBaseRepository<Tournament> TournamentRepository
        {
            get { return tournamentRepository ??= new TournamentRepository(context); }
        }
        public IBaseRepository<Sector> SectorsRepository
        {
            get { return sectorsRepository ??= new SectorRepository(context); }
        }

        public IBaseRepository<BookingSector> BookingSectorsRepository
        {
            get { return bookingRepository ??= new BookingSectorRepository(context); }
        }


        public async Task<bool> SaveAsync()
        {
            try
            {
                var changes = context.ChangeTracker.Entries().Count(
                    p => p.State == EntityState.Modified || p.State == EntityState.Deleted
                                                         || p.State == EntityState.Added);
                if (changes == 0) return true;

           
                return await context.SaveChangesAsync()>0;
            }
            catch
            {
                return false;
            }
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
