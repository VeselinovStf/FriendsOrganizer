using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FriendsOrganizer.Data.Models
{
    public class Friend 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50,ErrorMessage = "Invalid Firs Name",MinimumLength = 5)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Invalid Firs Name", MinimumLength = 5)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string FullName()
        {
            return this.FirstName + " " + this.LastName;
        }

      
    }
}
