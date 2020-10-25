using FriendsOrganizer.Data;
using FriendsOrganizer.Data.Abstraction;
using FriendsOrganizer.Data.Models;
using FriendsOrganizer.Data.Repositories;
using FriendsOrganizer.Friends.Service.Abstraction;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsOrganizer.Friends.Service
{
    public class FriendService : IFriendService
    {
        private readonly IGenericRepository<Friend, FriendsOrganizerDbContext> _friendRepository;

        public FriendService(
            IGenericRepository<Friend, FriendsOrganizerDbContext> friendRepository
            )
        {
            this._friendRepository = friendRepository;
        }

        public async Task<Friend> AddNewAsync()
        {
            var newFriend = new Friend();

            await this._friendRepository.AddAsync(newFriend);

            return newFriend;
        }

        public async Task<IEnumerable<Friend>> GetAllAsync()
        {
            var dbCall = await this._friendRepository
                .GetAllAsync();

            return dbCall.ToList();
        }

        public async Task<Friend> GetAsync(int id)
        {
            var dbCall = await this._friendRepository
                .GetAsync(id);

            if (dbCall == null)
            {
                throw new KeyNotFoundException("Friend not found");
            }
            return dbCall;           
        }

        public bool HasChanges()
        {
            return this._friendRepository.HasChanges();
        }

        public async Task RemoveAsync(Friend model)
        {
            this._friendRepository.Remove(model);
            await this._friendRepository.SaveChangesAsync();
        }

        public async Task UpdateFriendAsync()
        {
            await this._friendRepository.SaveChangesAsync();
        }
    }
}
