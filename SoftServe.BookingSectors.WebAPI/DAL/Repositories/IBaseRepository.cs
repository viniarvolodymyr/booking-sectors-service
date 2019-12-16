using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllEntitiesAsync();
        Task<T> GetEntityByIdAsync(int id);
        ValueTask<EntityEntry<T>> InsertEntityAsync(T entity);
        void UpdateEntity(T entity);
        Task<EntityEntry<T>> DeleteEntityByIdAsync(int id);
    }
}
