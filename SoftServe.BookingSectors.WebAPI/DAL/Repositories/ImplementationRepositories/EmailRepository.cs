using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SoftServe.BookingSectors.WebAPI.BLL.ErrorHandling;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementationRepositories
{
    public class EmailRepository : IBaseRepository<Email>
    {

        private readonly BookingSectorContext context;
        private readonly DbSet<Email> emailSet;

        public EmailRepository(BookingSectorContext context)
        {
            this.context = context;
            emailSet = context.Set<Email>();
        }
        public Task<List<Email>> GetAllEntitiesAsync()
        {
            return emailSet.AsNoTracking().ToListAsync();
        }
        public Task<Email> GetEntityByIdAsync(int id)
        {
            var result = emailSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
            if (result.Result == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"User-Email with id: {id} not found when trying to get entity.");
            }

            return result;
        }

        public IQueryable<Email> GetByCondition(Expression<Func<Email, bool>> expression)
        {
            return emailSet.Where(expression).AsNoTracking();
        }

        public async Task<Email> InsertEntityAsync(Email entityToInsert)
        {
            return (await emailSet.AddAsync(entityToInsert)).Entity;
        }

        public Email UpdateEntity(Email entityToUpdate)
        {
            return emailSet.Update(entityToUpdate).Entity;
        }
        public async Task<Email> DeleteEntityByIdAsync(int id)
        {
            var entityToDelete = await emailSet.FindAsync(id);
            if (entityToDelete == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"User-Email with id: {id} not found when trying to update entity. Entity was no Deleted.");
            }
            return emailSet.Remove(entityToDelete).Entity;
        }
    }
}
