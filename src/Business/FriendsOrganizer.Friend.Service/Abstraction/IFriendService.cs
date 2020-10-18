using FriendsOrganizer.Data.Models;
using FriendsOrganizer.Friends.Service.DTOs;
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
    }
}
