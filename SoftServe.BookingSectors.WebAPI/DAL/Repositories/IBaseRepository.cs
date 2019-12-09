using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllEntitiesAsync();
        Task<T> GetEntityByIdAsync(int id);
        ValueTask<EntityEntry<T>> InsertEntityAsync(T entity);
        void UpdateEntity(T entity);
        Task DeleteEntityByIdAsync(int id);
    }
}
