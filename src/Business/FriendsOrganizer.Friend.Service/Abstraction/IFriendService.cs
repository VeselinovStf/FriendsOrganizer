using FriendsOrganizer.Data.Models;
using System.Collections.Generic;

namespace FriendsOrganizer.Friends.Service.Abstraction
{
    public interface IFriendService
    {
        IEnumerable<Friend> GetAll();
    }
}
