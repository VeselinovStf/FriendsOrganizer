using FriendsOrganizer.Data.Abstraction;
using FriendsOrganizer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FriendsOrganizer.Data.Repositories
{
    public class FriendMeetingRepository : IFriendMeetingRepository
    {
        private readonly FriendsOrganizerDbContext _dbContext;

        public FriendMeetingRepository(FriendsOrganizerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<FriendMeeting> GetByFriendIdAsync(int friendId)
        {
            return await this._dbContext.FriendMeetings
                .FirstOrDefaultAsync(f => f.FriendId == friendId);
        }
    }
}
