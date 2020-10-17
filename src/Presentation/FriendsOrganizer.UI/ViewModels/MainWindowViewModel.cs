using FriendsOrganizer.UI.Models;
using System.Threading.Tasks;

namespace FriendsOrganizer.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public NavigationViewModel NavigationViewModel { get; }

        public MainWindowViewModel(NavigationViewModel navigationViewModel)
        {
            this.NavigationViewModel = navigationViewModel;
        }

        public async Task LoadAsync()
        {
            await this.NavigationViewModel.LoadAsync();
        }
    }
}
