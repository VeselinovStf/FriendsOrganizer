using AutoMapper;
using FriendsOrganizer.Data;
using FriendsOrganizer.Friends.Service.Abstraction;
using FriendsOrganizer.Friends.Service.DTOs;
using System.Collections.Generic;

namespace FriendsOrganizer.Friends.Service
{
    public class FriendService : IFriendService
    {
        private readonly FriendsOrganizerDbContext _dbContext;
        private readonly IMapper _mapper;

        public FriendService(
            FriendsOrganizerDbContext dbContext,
            IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public IEnumerable<FriendDTO> GetAll()
        {
            var dbCall = this._dbContext
                .Friends;

            var resultModel = this._mapper
                .Map<IList<FriendDTO>>(dbCall);
                
            return resultModel;
        }
    }
}
