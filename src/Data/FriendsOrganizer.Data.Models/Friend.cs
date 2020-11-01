using FriendsOrganizer.Data.Models.Abstraction;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FriendsOrganizer.Data.Models
{
    public class Friend : BaseEntity
    {
        public Friend()
        {
            this.PhoneNumbers = new HashSet<FriendPhoneNumber>();
            this.FriendMeetings = new HashSet<FriendMeeting>();
        }

        [Required]
        [StringLength(50,ErrorMessage = "Invalid Firs Name",MinimumLength = 5)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Invalid Firs Name", MinimumLength = 5)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int? ProgrammingLanguageId { get; set; }

        public ProgrammingLanguage ProgrammingLanguage { get; set; }

        public ICollection<FriendPhoneNumber> PhoneNumbers { get; set; }
        public ICollection<FriendMeeting> FriendMeetings { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public string FullName()
        {
            return this.FirstName + " " + this.LastName;
        }

      
    }
}
