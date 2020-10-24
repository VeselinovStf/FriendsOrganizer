using FriendsOrganizer.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendsOrganizer.Data.Abstraction
{
    public interface IAsyncRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        Task SaveChangesAsync();

        bool HasChanges();
        Task AddAsync(T newFriend);
        void Remove(T model);
    }
}
