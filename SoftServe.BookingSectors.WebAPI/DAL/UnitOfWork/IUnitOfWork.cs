using System;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories;

namespace SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<User> UserRepository { get; }
        IBaseRepository<Tournament> TournamentRepository { get; }
        IBaseRepository<Sector> SectorRepository { get; }
        IBaseRepository<BookingSector> BookingSectorRepository { get; }
        IBaseRepository<Setting> SettingRepository { get; }
        IBaseRepository<Token> TokenRepository { get; }
        IBaseRepository<Email> EmailRepository { get; }
        Task<bool> SaveAsync();
    }
}
