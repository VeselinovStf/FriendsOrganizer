using FriendsOrganizer.Friends.Service.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendsOrganizer.Friends.Service.Abstraction
{
    public interface IFriendService
    {
        Task<IEnumerable<FriendDTO>> GetAllAsync();

        Task<FriendDTO> GetAsync(int id);

        Task UpdateFriendAsync(FriendDTO updatableFriend);
     
    }
}
