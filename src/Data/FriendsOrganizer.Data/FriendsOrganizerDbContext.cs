using FriendsOrganizer.Data.Models;
using System.Collections.Generic;

namespace FriendsOrganizer.Data
{
    public class FriendsOrganizerDbContext
    {
        public ICollection<Friend> Friends { get; set; } = new List<Friend>()
        {
             new Friend() { FirstName = "John", LastName = "Dow", Email = "JohnD@mail.com" },
             new Friend() { FirstName = "Jayne", LastName = "Dow", Email = "JaybeD@mail.com" },
             new Friend() { FirstName = "Tom", LastName = "Cat", Email = "TomC@mail.com" },
             new Friend() { FirstName = "Jerry", LastName = "Mouce", Email = "JerryM@mail.com" }
        };
    }
}
