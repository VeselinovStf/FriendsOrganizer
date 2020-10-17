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

        }
    }
}
