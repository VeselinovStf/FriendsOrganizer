using FriendsOrganizer.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendsOrganizer.Friends.Service.Abstraction
{
    public interface IFriendService
    {
        Task<IEnumerable<Friend>> GetAllAsync();

        Task<Friend> GetAsync(int id);

        Task UpdateFriendAsync();
        bool HasChanges();
        Task<Friend> AddNewAsync();
        Task RemoveAsync(Friend model);
        Task<bool> HasMeetingAsync(int id);
        Task ReloadFriend(int id);
    }
}
