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
            var mainWindow = new MainWindow(
                new MainWindowViewModel(
                    new FriendService(
                        new FriendsOrganizerDbContext())));

            mainWindow.Show();
        }
    }
}
