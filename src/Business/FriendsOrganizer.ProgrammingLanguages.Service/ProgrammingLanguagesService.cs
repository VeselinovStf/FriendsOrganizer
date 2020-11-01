using FriendsOrganizer.Data;
using FriendsOrganizer.Data.Abstraction;
using FriendsOrganizer.Data.Models;
using FriendsOrganizer.ProgrammingLanguages.Service.Abstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendsOrganizer.ProgrammingLanguages.Service
{
    public class ProgrammingLanguagesService : IProgrammingLanguagesService
    {
        private readonly IGenericRepository<ProgrammingLanguage,FriendsOrganizerDbContext> _programmingLanguageRepository;
        private readonly IProgrammingLanguageFriendRepository _programmingLanguageFriendRepository;

        public ProgrammingLanguagesService(
            IGenericRepository<ProgrammingLanguage, FriendsOrganizerDbContext> programmingLanguageRepository,
            IProgrammingLanguageFriendRepository programmingLanguageFriendRepository)
        {
            this._programmingLanguageRepository = programmingLanguageRepository;
            this._programmingLanguageFriendRepository = programmingLanguageFriendRepository;
        }

        public async Task AddAsync(ProgrammingLanguage model)
        {
            await this._programmingLanguageRepository.AddAsync(model);
        }

        public async Task<IEnumerable<ProgrammingLanguage>> GetAllAsync()
        {
            return await this._programmingLanguageRepository
                .GetAllAsync();
        }

        public bool HasChanges()
        {
            return this._programmingLanguageRepository.HasChanges();
        }

        public async Task<bool> IsReferencedAsync(int id)
        {
            return await this._programmingLanguageFriendRepository
                .IsReferenceAsync(id);
        }

        public void Remove(ProgrammingLanguage model)
        {
            this._programmingLanguageRepository.Remove(model);
        }

        public async Task SaveAsync()
        {
            await this._programmingLanguageRepository.SaveChangesAsync();
        }
    }
}
