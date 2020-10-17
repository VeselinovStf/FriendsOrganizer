

using Autofac;
using FriendsOrganizer.Data;
using FriendsOrganizer.Friends.Service;
using FriendsOrganizer.Friends.Service.Abstraction;
using FriendsOrganizer.UI.ViewModels;
using FriendsOrganizer.UI.Views;

namespace FriendsOrganizer.UI.DI
{
    public class AppBootstrapModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindowViewModel>().AsSelf();
            builder.RegisterType<FriendsOrganizerDbContext>().AsSelf().SingleInstance();
            builder.RegisterType<FriendService>().As<IFriendService>();
            builder.RegisterType<MainWindow>().AsSelf();
        }
    }
}
