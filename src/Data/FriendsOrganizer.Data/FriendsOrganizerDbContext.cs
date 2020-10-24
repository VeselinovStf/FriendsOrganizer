using FriendsOrganizer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FriendsOrganizer.Data
{
    public class FriendsOrganizerDbContext : DbContext
    {
        public DbSet<Friend> Friends { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<FriendPhoneNumber> FriendsPhonesNumbers { get; set; }

        public FriendsOrganizerDbContext(DbContextOptions<FriendsOrganizerDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }


    }
}
