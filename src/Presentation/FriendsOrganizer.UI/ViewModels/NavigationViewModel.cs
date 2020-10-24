using FriendsOrganizer.Friends.Service.Abstraction;
using FriendsOrganizer.UI.Events;
using FriendsOrganizer.UI.ViewModels;
using Prism.Events;
using System;
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

            this._eventAggregator.GetEvent<AfterFriendDeleteEvent>()
                .Subscribe(AfterFriendDeleteHandler);
        }

        private void AfterFriendDeleteHandler(int friendId)
        {
            var friend = this.Friends
                .FirstOrDefault(f => f.Id == friendId);

            if (friend != null)
            {
                Friends.Remove(friend);
            }
        }

        private void AfterSaveFriendEventHandler(AfterFriendSaveDetailsLookup savedFriend)
        {
            var friend = this.Friends
                .FirstOrDefault(f => f.Id == savedFriend.Id);

            if (friend == null)
            {
                Friends.Add(
                    new NavigationViewItemModel(
                        savedFriend.Id, 
                        savedFriend.DisplayProperty, 
                        this._eventAggregator));
            }
            else
            {
                friend.DisplayProperty = savedFriend.DisplayProperty;
            }
            
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
