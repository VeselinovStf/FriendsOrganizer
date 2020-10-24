using FriendsOrganizer.Data.Models.Abstraction;

namespace FriendsOrganizer.Data.Models
{
    public class ProgrammingLanguage : BaseEntity
    {
        public string Name { get; set; }

        public int? FriendId { get; set; }

        public Friend Friend { get; set; }
    }
}
