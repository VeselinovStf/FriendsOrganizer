using FriendsOrganizer.Friends.Service.DTOs;
using System.Collections.Generic;

namespace FriendsOrganizer.Friends.Service.Abstraction
{
    public interface IFriendService
    {
        IEnumerable<FriendDTO> GetAll();
    }
}
