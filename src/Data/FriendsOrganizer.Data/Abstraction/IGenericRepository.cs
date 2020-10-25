using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendsOrganizer.Data.Abstraction
{
    public interface IGenericRepository<TEntity, TContext>      
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(int id);

        Task SaveChangesAsync();

        bool HasChanges();
        Task AddAsync(TEntity newFriend);
        void Remove(TEntity model);
    }
}
