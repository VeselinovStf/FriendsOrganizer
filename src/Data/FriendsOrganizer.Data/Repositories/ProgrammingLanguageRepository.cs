using FriendsOrganizer.Data.Models;

namespace FriendsOrganizer.Data.Repositories
{
    public class ProgrammingLanguageRepository : GenericRepository<ProgrammingLanguage, FriendsOrganizerDbContext>
    {
        public ProgrammingLanguageRepository(FriendsOrganizerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
