using FriendsOrganizer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FriendsOrganizer.UI.DI
{
    public static class FriendsOrganizerDbContextFactory
    {
        public static DbContextOptionsBuilder<FriendsOrganizerDbContext> OptionsBuilder()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FriendsOrganizerDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-UGHAA7O;Database=FriendsDb;Trusted_Connection=True;");

            return optionsBuilder;
        }
    }

}
