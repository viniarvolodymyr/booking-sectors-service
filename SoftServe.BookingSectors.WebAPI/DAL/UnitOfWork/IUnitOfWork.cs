using System;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories;

namespace SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
      
        IBaseRepository<Tournament> tournamentRepositoty { get; }
        IBaseRepository<TournamentSector> TournamentSectors { get; }
        Task<bool>  SaveAsync();
    }
}
