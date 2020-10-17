using FriendsOrganizer.Data.Models;
using FriendsOrganizer.Friends.Service.Abstraction;
using System.Collections.Generic;

namespace FriendsOrganizer.Friends.Service
{
    public class FriendService : IFriendService
    {
        public IEnumerable<Friend> GetAll()
        {
            yield return new Friend() { FirstName = "John", LastName = "Dow", Email = "JohnD@mail.com" };
            yield return new Friend() { FirstName = "Jayne", LastName = "Dow", Email = "JaybeD@mail.com" };
            yield return new Friend() { FirstName = "Tom", LastName = "Cat", Email = "TomC@mail.com" };
            yield return new Friend() { FirstName = "Jerry", LastName = "Mouce", Email = "JerryM@mail.com" };
        }
    }
}
