using System;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories;

namespace SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
<<<<<<< HEAD
        IBaseRepository<Sector> Sectors { get; }
        IBaseRepository<BookingSector> BookingSectors { get; }
        void Save();
=======
        IBaseRepository<Sector> SectorsRepository { get; }
        Task<bool>  SaveAsync();
>>>>>>> master
    }
}
