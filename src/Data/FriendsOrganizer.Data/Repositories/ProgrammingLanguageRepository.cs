using FriendsOrganizer.Data.Abstraction;
using FriendsOrganizer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendsOrganizer.Data.Repositories
{
    public class ProgrammingLanguageRepository : IAsyncRepository<ProgrammingLanguage>
    {
        private readonly FriendsOrganizerDbContext _dbContext;

        public ProgrammingLanguageRepository(FriendsOrganizerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddAsync(ProgrammingLanguage newFriend)
        {
            await this._dbContext.AddAsync(newFriend);
        }

        public async Task<IEnumerable<ProgrammingLanguage>> GetAllAsync()
        {
            return await this._dbContext
                .ProgrammingLanguages
                .ToListAsync();
        }

        public async Task<ProgrammingLanguage> GetAsync(int id)
        {
            return await this._dbContext
                .ProgrammingLanguages
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public bool HasChanges()
        {
            return this._dbContext.ChangeTracker.HasChanges();
        }

        public void Remove(ProgrammingLanguage model)
        {
            this._dbContext.ProgrammingLanguages.Remove(model);
        }

        public async Task SaveChangesAsync()
        {
            await this._dbContext.SaveChangesAsync();
        }
    }
}
