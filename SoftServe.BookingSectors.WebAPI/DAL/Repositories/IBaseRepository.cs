using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllEntitiesAsync();
        Task<T> GetEntityByIdAsync(int id);
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
        Task<T> InsertEntityAsync(T entity);
        T UpdateEntity(T entity);
        Task<T> DeleteEntityByIdAsync(int id);
    }
}
