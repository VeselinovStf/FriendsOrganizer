using FriendsOrganizer.Data.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FriendsOrganizer.Data.Models
{
    public class Meeting : BaseEntity
    {
        public Meeting()
        {
            this.FriendMeetings = new HashSet<FriendMeeting>();
        }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Invalid Meeting title")]
        public string Title { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public ICollection<FriendMeeting> FriendMeetings { get; set; }
    }
}
