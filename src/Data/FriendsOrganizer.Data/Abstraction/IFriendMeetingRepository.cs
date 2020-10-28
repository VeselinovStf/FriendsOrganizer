using FriendsOrganizer.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FriendsOrganizer.Data.Abstraction
{
    public interface IFriendMeetingRepository
    {
        Task<FriendMeeting> GetByFriendIdAsync(int friendId);
    }
}
