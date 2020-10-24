using System.ComponentModel.DataAnnotations;

namespace FriendsOrganizer.Data.Models.Abstraction
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
