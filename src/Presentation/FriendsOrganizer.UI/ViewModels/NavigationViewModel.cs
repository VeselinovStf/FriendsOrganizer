using FriendsOrganizer.Friends.Service.Abstraction;
using FriendsOrganizer.UI.Events;
using FriendsOrganizer.UI.ViewModels;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsOrganizer.UI.Models
{
    public class NavigationViewModel : ViewModelBase
    {
        private readonly IFriendService _friendService;
        private readonly IEventAggregator _eventAggregator;

        public NavigationViewModel(
            IFriendService friendService,
            IEventAggregator eventAggregator)
        {
            this._friendService = friendService;
            this._eventAggregator = eventAggregator;
            this.Friends = new ObservableCollection<NavigationViewItemModel>();

            this._eventAggregator.GetEvent<AfterFriendSaveDetailsEvent>()
                .Subscribe(AfterSaveFriendEventHandler);
        }

        private void AfterSaveFriendEventHandler(AfterFriendSaveDetailsLookup savedFriend)
        {
            var friend = this.Friends
                .FirstOrDefault(f => f.Id == savedFriend.Id);

            friend.DisplayProperty = savedFriend.DisplayProperty;
        }

        public async Task LoadAsync()
        {
            var friendsLookupServiceCall = await this._friendService
                .GetAllAsync();

            Friends.Clear();

            foreach (var friend in friendsLookupServiceCall)
            {
                Friends.Add(new NavigationViewItemModel(friend.Id, friend.FullName(), this._eventAggregator ));
            }
        }
        public ObservableCollection<NavigationViewItemModel> Friends { get; set; }

    }
}
