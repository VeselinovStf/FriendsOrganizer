using FriendsOrganizer.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace FriendsOrganizer.Data
{
    public static class FriendsOrganizerDbSeed
    {
        public static void Seed(this FriendsOrganizerDbContext dbContext)
        {
            if (!dbContext.Friends.Any())
            {
                var newFriends = new List<Friend>()
                {
                     new Friend() {FirstName = "John", LastName = "Dow", Email = "JohnD@mail.com" },
                     new Friend() {FirstName = "Jayne", LastName = "Dow", Email = "JaybeD@mail.com" },
                     new Friend() {FirstName = "Tom", LastName = "Cat", Email = "TomC@mail.com" },
                     new Friend() {FirstName = "Jerry", LastName = "Mouce", Email = "JerryM@mail.com" }
                };

                dbContext.Friends.AddRange(newFriends);
                dbContext.SaveChanges();
            }

            if (!dbContext.ProgrammingLanguages.Any())
            {
                var newProgrammingLanguages = new List<ProgrammingLanguage>()
                {
                    new ProgrammingLanguage(){Name = "C#"},
                    new ProgrammingLanguage(){Name = "Java"},
                    new ProgrammingLanguage(){Name = "F#"},
                    new ProgrammingLanguage(){Name = "C++"},
                    new ProgrammingLanguage(){Name = "C"},
                };

                dbContext.ProgrammingLanguages.AddRange(newProgrammingLanguages);
                dbContext.SaveChangesAsync();
            }

            if (!dbContext.FriendsPhonesNumbers.Any())
            {
                var newPhoneNumber = new FriendPhoneNumber() { PhoneNumber = "+35988765342", FriendId = dbContext.Friends.FirstOrDefault().Id };

                dbContext.FriendsPhonesNumbers.AddAsync(newPhoneNumber);
                dbContext.SaveChangesAsync();
            }

        }
    }
}
