using FriendsOrganizer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FriendsOrganizer.Data.Repositories
{
    public class MeetingRepository : GenericRepository<Meeting, FriendsOrganizerDbContext>
    {
        public MeetingRepository(FriendsOrganizerDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Meeting> GetAsync(int id)
        {
            return await base._dbContext
                .Meetings
                .Include(m => m.FriendMeetings)
                    .ThenInclude(fm => fm.Friend)
                .FirstOrDefaultAsync(m => m.Id == id);

        }
    }
}
