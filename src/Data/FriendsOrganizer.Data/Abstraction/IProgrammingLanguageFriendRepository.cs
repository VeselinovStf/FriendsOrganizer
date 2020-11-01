using System.Threading.Tasks;

namespace FriendsOrganizer.Data.Abstraction
{
    public interface IProgrammingLanguageFriendRepository
    {
        Task<bool> IsReferenceAsync(int id);
    }
}
