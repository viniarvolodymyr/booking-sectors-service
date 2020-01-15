using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SoftServe.BookingSectors.WebAPI.BLL.ErrorHandling;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers.LoggerManager;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementationRepositories;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingSectorContext context;
        private SettingRepository settingsRepository;
        private SectorRepository sectorRepository;
        private UserRepository userRepository;
        private TournamentRepository tournamentRepository;
        private BookingSectorRepository bookingRepository;
        private TokenRepository tokenRepository;

        private readonly ILoggerManager logger;


        public UnitOfWork(BookingSectorContext context, ILoggerManager logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public IBaseRepository<BookingSector> BookingSectorRepository =>
            bookingRepository ??= new BookingSectorRepository(context);
        public IBaseRepository<Sector> SectorRepository =>
            sectorRepository ??= new SectorRepository(context);
        public IBaseRepository<Setting> SettingRepository =>
            settingsRepository ??= new SettingRepository(context);
        public IBaseRepository<Tournament> TournamentRepository =>
            tournamentRepository ??= new TournamentRepository(context);
        public IBaseRepository<User> UserRepository =>
            userRepository ??= new UserRepository(context);

        public IBaseRepository<Token> TokenRepository =>
         tokenRepository ??= new TokenRepository(context);

        public async Task<bool> SaveAsync()
        {
            try
            {
                var changes = context.ChangeTracker.Entries().Count(
                    p => p.State == EntityState.Modified || p.State == EntityState.Deleted
                                                         || p.State == EntityState.Added);
               
                return changes == 0 || await context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException e)
            {
                var sqlExc = e.GetBaseException() as SqlException;

              
                if (sqlExc?.Number == 547)
                {        
                    logger.LogError($"Number of sql exception: {sqlExc.Number.ToString()}");
                    throw new ConstraintException("There are dependencies for selected entity, delete them firstly.", sqlExc.InnerException);                    
                }
                logger.LogError($"{e}, {nameof(SaveAsync)}, {e.Entries}");
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, e.InnerException);
            }
            catch (Exception e)
            {
                logger.LogError($"{e}, {nameof(SaveAsync)}");
                return false;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
