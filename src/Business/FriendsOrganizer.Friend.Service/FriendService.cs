using AutoMapper;
using FriendsOrganizer.Data;
using FriendsOrganizer.Friends.Service.Abstraction;
using FriendsOrganizer.Friends.Service.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<FriendDTO>> GetAllAsync()
        {
            var dbCall = await this._dbContext
                .Friends
                .AsNoTracking()
                .ToListAsync();

            var resultModel = this._mapper
                .Map<IList<FriendDTO>>(dbCall);

            return resultModel;
        }

    }
}
