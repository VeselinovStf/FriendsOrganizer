using System.ComponentModel.DataAnnotations;

namespace FriendsOrganizer.UI.Models
{
    public class FriendModel
    {
        public int Id { get; set; }


        [Display(Name = "First Name")]
        [Required]
        [StringLength(50, ErrorMessage = "Invalid Last Name", MinimumLength = 4)]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        [Required]
        [StringLength(50,ErrorMessage = "Invalid Last Name",MinimumLength = 4)]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
