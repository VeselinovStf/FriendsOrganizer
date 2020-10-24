using FriendsOrganizer.Data.Abstraction;
using FriendsOrganizer.Data.Models;
using FriendsOrganizer.ProgrammingLanguages.Service.Abstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendsOrganizer.ProgrammingLanguages.Service
{
    public class ProgrammingLanguagesService : IProgrammingLanguagesService
    {
        private readonly IAsyncRepository<ProgrammingLanguage> _programmingLanguageRepository;

        public ProgrammingLanguagesService(IAsyncRepository<ProgrammingLanguage> programmingLanguageRepository)
        {
            this._programmingLanguageRepository = programmingLanguageRepository;
        }
        public async Task<IEnumerable<ProgrammingLanguage>> GetAllAsync()
        {
            return await this._programmingLanguageRepository
                .GetAllAsync();
        }
    }
}
