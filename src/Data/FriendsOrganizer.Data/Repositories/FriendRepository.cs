using FriendsOrganizer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FriendsOrganizer.Data.Repositories
{
    public class FriendRepository : 
        GenericRepository<Friend, FriendsOrganizerDbContext>
        
    {
        public FriendRepository(FriendsOrganizerDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Friend> GetAsync(int id)
        {
            return await base._dbContext.Friends
              .Include(f => f.PhoneNumbers)
              .FirstOrDefaultAsync(f => f.Id == id);
        }
     
    }
}
