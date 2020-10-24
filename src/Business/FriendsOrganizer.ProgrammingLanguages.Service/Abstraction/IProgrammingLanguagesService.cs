using FriendsOrganizer.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendsOrganizer.ProgrammingLanguages.Service.Abstraction
{
    public interface IProgrammingLanguagesService
    {
        Task<IEnumerable<ProgrammingLanguage>> GetAllAsync();
    }
}
