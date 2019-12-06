using System;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories;

namespace SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Sector> Sectors { get; }

        IBaseRepository<User> Users { get; }
        IBaseRepository<Tournament> Tournaments { get; }
        IBaseRepository<TournamentSector> TournamentSectors { get; }
        void Save();
    }
}
