using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllEntitiesAsync();
        Task<T> GetEntityByIdAsync(int id);
        Task InsertEntityAsync(T entity);
        void UpdateEntity(T entity);
        Task DeleteEntityAsync(int Id);
    }
}
