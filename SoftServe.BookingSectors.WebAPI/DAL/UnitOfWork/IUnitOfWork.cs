using System;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories.Interfaces;

namespace SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<User> UsersRepository { get; }
        IBaseRepository<TournamentSector> TournamentSectorsRepository { get; }
        IBaseRepository<Tournament> TournamentRepository { get; }
        IBaseRepository<Sector> SectorsRepository { get; }
        IBaseRepository<BookingSector> BookingSectorsRepository { get; }
        IBaseRepository<Setting> Settings { get;  }
        Task<bool>  SaveAsync();
    }
}
