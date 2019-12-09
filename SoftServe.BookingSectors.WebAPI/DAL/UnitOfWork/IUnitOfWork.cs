using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories;
using System;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Sector> SectorsRepository { get; }
        IBaseRepository<BookingSector> BookingSectorsRepository { get; }
        Task<bool>SaveAsync();
    }
}
