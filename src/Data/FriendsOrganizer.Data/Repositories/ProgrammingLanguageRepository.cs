using FriendsOrganizer.Data.Abstraction;
using FriendsOrganizer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FriendsOrganizer.Data.Repositories
{
    public class ProgrammingLanguageRepository : 
        GenericRepository<ProgrammingLanguage, FriendsOrganizerDbContext>,
        IProgrammingLanguageFriendRepository
    {
        public ProgrammingLanguageRepository(FriendsOrganizerDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsReferenceAsync(int id)
        {
            return await base._dbContext.Friends.AsNoTracking()
                .AnyAsync(f => f.ProgrammingLanguageId == id);
        }
    }
}
