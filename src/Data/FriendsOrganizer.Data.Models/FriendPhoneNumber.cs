using FriendsOrganizer.Data.Models.Abstraction;
using System.ComponentModel.DataAnnotations;

namespace FriendsOrganizer.Data.Models
{
    public class FriendPhoneNumber : BaseEntity
    {
        [Phone]
        public string PhoneNumber { get; set; }

        public int FriendId { get; set; }

        public Friend Friend { get; set; }
    }
}
