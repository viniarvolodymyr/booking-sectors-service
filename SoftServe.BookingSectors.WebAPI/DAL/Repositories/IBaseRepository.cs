using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllEntitiesAsync();
        Task<T> GetEntityAsync(int id);
        Task InsertEntityAsync(T entity);
        void UpdateEntity(T entity);
        Task DeleteEntityAsync(int Id);
        Task SaveEntityAsync();
    }
}
