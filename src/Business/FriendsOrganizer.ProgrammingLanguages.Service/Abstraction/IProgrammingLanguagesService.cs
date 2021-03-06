﻿using FriendsOrganizer.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendsOrganizer.ProgrammingLanguages.Service.Abstraction
{
    public interface IProgrammingLanguagesService
    {
        Task<IEnumerable<ProgrammingLanguage>> GetAllAsync();
        bool HasChanges();
        Task SaveAsync();
        void Remove(ProgrammingLanguage model);
        Task AddAsync(ProgrammingLanguage model);
        Task<bool> IsReferencedAsync(int id);
    }
}
