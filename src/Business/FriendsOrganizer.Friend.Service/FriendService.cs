using FriendsOrganizer.Data;
using FriendsOrganizer.Friends.Service.Abstraction;
using FriendsOrganizer.Friends.Service.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace FriendsOrganizer.Friends.Service
{
    public class FriendService : IFriendService
    {
        private readonly FriendsOrganizerDbContext _dbContext;

        public FriendService(FriendsOrganizerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<FriendDTO> GetAll()
        {
            var dbCall = this._dbContext
                .Friends;

            var resultModel = new List<FriendDTO>(dbCall.Select(f => new FriendDTO()
            {
                Email = f.Email,
                FirstName = f.FirstName,
                Id = f.Id,
                LastName = f.LastName
            }));

            return resultModel;
        }
    }
}
