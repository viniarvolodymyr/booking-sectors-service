using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        ValueTask<EntityEntry<T>> InsertEntityAsync(T entity);
        void UpdateEntity(T entity);
        Task<EntityEntry<T>> DeleteEntityByIdAsync(int id);
    }
}
