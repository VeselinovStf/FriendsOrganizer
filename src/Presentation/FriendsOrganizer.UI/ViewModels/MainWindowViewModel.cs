using FriendsOrganizer.UI.Models;
using System.Threading.Tasks;

namespace FriendsOrganizer.UI.ViewModels
{
    public class MainWindowViewModel
    {
        public FriendDetailViewModel FriendDetailViewModel { get; }

        public NavigationViewModel NavigationViewModel { get; }

        public MainWindowViewModel(
            NavigationViewModel navigationViewModel,
            FriendDetailViewModel friendDetailViewModel)
        {
            this.NavigationViewModel = navigationViewModel;
            this.FriendDetailViewModel = friendDetailViewModel;
        }

        public async Task LoadAsync()
        {
            await this.NavigationViewModel.LoadAsync();
        }
    }
}
