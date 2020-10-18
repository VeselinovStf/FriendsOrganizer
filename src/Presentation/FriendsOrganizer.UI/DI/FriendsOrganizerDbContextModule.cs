using Autofac;
using FriendsOrganizer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FriendsOrganizer.UI.DI
{
    public class FriendsOrganizerDbContextModule : Module
    {
        public class FriendsOrganizerDbContextFactory : IDesignTimeDbContextFactory<FriendsOrganizerDbContext>
        {
            public FriendsOrganizerDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<FriendsOrganizerDbContext>();
                optionsBuilder.UseSqlServer("Server=DESKTOP-UGHAA7O;Database=FriendsDb;Trusted_Connection=True;");

                return new FriendsOrganizerDbContext(optionsBuilder.Options);
            }       
        }   

        protected override void Load(ContainerBuilder builder)
        {

            builder.Register(c =>
            {
                var opt = new DbContextOptionsBuilder<FriendsOrganizerDbContext>();
                opt.UseSqlServer("Server=DESKTOP-UGHAA7O;Database=FriendsDb;Trusted_Connection=True;");

                return new FriendsOrganizerDbContext(opt.Options);
            }).AsImplementedInterfaces()
            .AsSelf();
        }

      
    }

}
