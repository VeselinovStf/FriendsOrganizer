using FriendsOrganizer.Data.Models.Abstraction;
using System.ComponentModel.DataAnnotations;

namespace FriendsOrganizer.Data.Models
{
    public class ProgrammingLanguage : BaseEntity
    {
        [Required]
        [StringLength(50,MinimumLength =1,ErrorMessage = "Invalid Programming Language Name")]
        public string Name { get; set; }

        
    }
}
