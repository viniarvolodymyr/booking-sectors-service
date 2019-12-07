using System;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Sector> SectorsRepository { get; }
        IBaseRepository<User> User { get; }
        Task<int> SaveAsync();
    }
}
