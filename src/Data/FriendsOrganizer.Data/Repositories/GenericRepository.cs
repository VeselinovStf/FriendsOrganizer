using FriendsOrganizer.Data.Abstraction;
using FriendsOrganizer.Data.Models.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendsOrganizer.Data.Repositories
{
    public class GenericRepository<TEntity, TContext> :
        IGenericRepository<TEntity, TContext>
                        where TEntity : BaseEntity
                        where TContext : DbContext
    {
        protected readonly TContext _dbContext;

        public GenericRepository(TContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            await this._dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this._dbContext.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await this._dbContext.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public bool HasChanges()
        {
            return this._dbContext.ChangeTracker.HasChanges();
        }

        public async Task ReloadEntity(int id)
        {
            var entry = await this._dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);

            if (entry != null)
            {
                await this._dbContext.Entry<TEntity>(entry).ReloadAsync();

            }
        }

        public void Remove(TEntity model)
        {
            this._dbContext.Set<TEntity>().Remove(model);
        }

        public async Task SaveChangesAsync()
        {
            await this._dbContext.SaveChangesAsync();
        }
    }
}
