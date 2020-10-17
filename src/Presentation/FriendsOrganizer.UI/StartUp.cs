using Autofac;
using FriendsOrganizer.UI.DI;

namespace FriendsOrganizer.UI
{
    public class StartUp
    {
        public IContainer OnConfiguration(ContainerBuilder builder)
        {
            builder.RegisterModule(new AppBootstrapModule());

            return builder.Build();
        }
    }
}
