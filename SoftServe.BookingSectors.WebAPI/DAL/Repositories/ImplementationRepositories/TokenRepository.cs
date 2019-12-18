using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public ValueTask<EntityEntry<Token>> InsertEntityAsync(Token entityToInsert)
        {
            return tokenSet.AddAsync(entityToInsert);
        }

        public void UpdateEntity(Token entityToUpdate)
        {
            tokenSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async Task<EntityEntry<Token>> DeleteEntityByIdAsync(int id)
        {
            Token tokenToDelete = await tokenSet.FindAsync(id);
            return tokenSet.Remove(tokenToDelete);
        }

    }