using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using System;

using System.Linq;
using System.Linq.Expressions;


namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllEntitiesAsync();
        Task<T> GetEntityAsync(int id);
        Task InsertEntityAsync(T entity);
        void UpdateEntity(T entity);
        Task DeleteEntityAsync(int id);
       
    }
}
