using Autofac;
using FriendsOrganizer.Data;
using Microsoft.EntityFrameworkCore;

namespace FriendsOrganizer.UI.DI
{
    public class FriendsOrganizerDbContextModule : Module
    {
        public DbContextOptionsBuilder<FriendsOrganizerDbContext> OptionsBuilder()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FriendsOrganizerDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-UGHAA7O;Database=FriendsDb;Trusted_Connection=True;");

            return optionsBuilder;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<FriendsOrganizerDbContext>(c =>
              new FriendsOrganizerDbContext(OptionsBuilder().Options))
              .InstancePerLifetimeScope();
        }
    }

}
