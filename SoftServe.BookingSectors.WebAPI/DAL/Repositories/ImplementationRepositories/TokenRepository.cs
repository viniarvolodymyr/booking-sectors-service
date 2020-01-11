using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementationRepositories
{
    public class TokenRepository : IBaseRepository<Token>
    {
        private readonly BookingSectorContext context;
        private readonly DbSet<Token> tokenSet;

        public TokenRepository(BookingSectorContext context)
        {
            this.context = context;
            tokenSet = context.Set<Token>();
        }

        public Task<List<Token>> GetAllEntitiesAsync()
        {
            return tokenSet.AsNoTracking().ToListAsync();
        }

        public Task<Token> GetEntityByIdAsync(int id)
        {
            return tokenSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Token> InsertEntityAsync(Token entityToInsert)
        {
            return (await tokenSet.AddAsync(entityToInsert)).Entity;
        }
        public IQueryable<Token> GetByCondition(Expression<Func<Token, bool>> expression)
        {
            return tokenSet.Where(expression).AsNoTracking().AsQueryable();
        }
        public void UpdateEntity(Token entityToUpdate)
        {
            tokenSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async Task<Token> DeleteEntityByIdAsync(int id)
        {
            Token tokenToDelete = await tokenSet.FindAsync(id);
            return tokenSet.Remove(tokenToDelete).Entity;
        }

    }
}