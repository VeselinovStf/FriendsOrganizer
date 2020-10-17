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

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"{e.Exception.Message} " +
                $"---------- " +
                $"{e.Exception.InnerException}", "ERROR OCCURES", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
