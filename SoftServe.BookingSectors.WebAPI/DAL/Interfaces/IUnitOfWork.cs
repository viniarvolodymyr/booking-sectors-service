using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
namespace SoftServe.BookingSectors.WebAPI.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Sector> Sectors { get; }
        void Save();
    }
}
