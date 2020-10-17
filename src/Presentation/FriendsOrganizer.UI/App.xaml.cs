using Autofac;
using FriendsOrganizer.Data;
using FriendsOrganizer.Friends.Service;
using FriendsOrganizer.UI.ViewModels;
using FriendsOrganizer.UI.Views;
using System.Windows;

namespace FriendsOrganizer.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var containerBuilder = new ContainerBuilder();
            var configurationStartUp = new StartUp();
            var container = configurationStartUp.OnConfiguration(containerBuilder);

            var mainWindow = container.Resolve<MainWindow>();

            mainWindow.Show();
        }
    }
}
