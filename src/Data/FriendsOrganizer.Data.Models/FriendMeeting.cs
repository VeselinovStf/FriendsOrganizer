namespace FriendsOrganizer.Data.Models
{
    public class FriendMeeting
    {
        public int MeetingId { get; set; }

        public Meeting Meeting { get; set; }

        public int FriendId { get; set; }

        public Friend Friend { get; set; }
    }
}
