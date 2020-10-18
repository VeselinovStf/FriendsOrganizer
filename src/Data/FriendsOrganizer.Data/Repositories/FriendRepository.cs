using FriendsOrganizer.Data.Abstraction;
using FriendsOrganizer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendsOrganizer.Data.Repositories
{
    public class FriendRepository : IAsyncRepository<Friend>
    {
        private readonly FriendsOrganizerDbContext _dbContext;

        public FriendRepository(FriendsOrganizerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<Friend>> GetAllAsync()
        {
            return await this._dbContext
                .Friends
                .ToListAsync();
        }

        public async Task<Friend> GetAsync(int id)
        {
            return await this._dbContext
                .Friends
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await this._dbContext.SaveChangesAsync();
        }
    }
}
