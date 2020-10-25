using FriendOrganizer.Meetings.Service.Abstraction;
using FriendsOrganizer.Data;
using FriendsOrganizer.Data.Abstraction;
using FriendsOrganizer.Data.Models;
using System.Threading.Tasks;

namespace FriendOrganizer.Meetings.Service
{
    public class MeetingService : IMeetingService
    {
        private readonly IGenericRepository<Meeting, FriendsOrganizerDbContext> _meetingRepository;

        public MeetingService(
             IGenericRepository<Meeting, FriendsOrganizerDbContext> meetingRepository)
        {
            this._meetingRepository = meetingRepository;
        }

        public async Task AddAsync(Meeting newMeeting)
        {
             await this._meetingRepository.AddAsync(newMeeting);
        }

        public async Task<Meeting> GetAsync(int id)
        {
            return await this._meetingRepository
                .GetAsync(id);
        }

        public bool HasChanges()
        {
            return this._meetingRepository.HasChanges();
        }

        public void Remove(Meeting model)
        {
            this._meetingRepository.Remove(model);
        }

        public async Task SaveChangesAsync()
        {
            await this._meetingRepository.SaveChangesAsync();
        }
    }
}
