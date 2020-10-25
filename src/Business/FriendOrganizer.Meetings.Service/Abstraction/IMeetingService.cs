using FriendsOrganizer.Data.Models;
using System.Threading.Tasks;

namespace FriendOrganizer.Meetings.Service.Abstraction
{
    public interface IMeetingService
    {
        Task<Meeting> GetAsync(int id);
        Task SaveChangesAsync();
        bool HasChanges();
        void Remove(Meeting model);
        Task AddAsync(Meeting newMeeting);
    }
}
