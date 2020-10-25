using Autofac;
using FriendsOrganizer.Data;
using FriendsOrganizer.Data.Abstraction;
using FriendsOrganizer.Data.Models;
using FriendsOrganizer.Data.Repositories;
using FriendsOrganizer.Friends.Service;
using FriendsOrganizer.Friends.Service.Abstraction;
using FriendsOrganizer.ProgrammingLanguages.Service;
using FriendsOrganizer.ProgrammingLanguages.Service.Abstraction;
using FriendsOrganizer.UI.Models;
using FriendsOrganizer.UI.UIServices;
using FriendsOrganizer.UI.ViewModels;
using FriendsOrganizer.UI.ViewModels.Abstraction;
using FriendsOrganizer.UI.Views;
using Prism.Events;

namespace FriendsOrganizer.UI.DI
{

    public class AppBootstrapModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindowViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().AsSelf();
            builder.RegisterType<FriendDetailViewModel>().As<IFriendDetailViewModel>();
            //builder.RegisterType<FriendDetailViewModel>().As<IDetailViewModel>();

            builder.RegisterType<FriendService>().As<IFriendService>();
            builder.RegisterType<ProgrammingLanguagesService>().As<IProgrammingLanguagesService>();
            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();

            builder.RegisterType<MainWindow>().AsSelf();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<FriendRepository>().As<IGenericRepository<Friend, FriendsOrganizerDbContext>>();
            builder.RegisterType<ProgrammingLanguageRepository>().As<IGenericRepository<ProgrammingLanguage, FriendsOrganizerDbContext>>();
        }
    }
}
