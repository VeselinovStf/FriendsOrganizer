using FriendsOrganizer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;

namespace FriendsOrganizer.Data
{
    public class FriendsOrganizerDbContext : DbContext
    {
        public DbSet<Friend> Friends { get; set; }

        public FriendsOrganizerDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

       
    }
}
