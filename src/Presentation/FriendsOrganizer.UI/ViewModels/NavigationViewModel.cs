using FriendsOrganizer.Friends.Service.Abstraction;
using FriendsOrganizer.UI.Events;
using FriendsOrganizer.UI.Events.Arguments;
using FriendsOrganizer.UI.UIServices;
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
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            this._friendService = friendService;
            this._eventAggregator = eventAggregator;
            this.Friends = new ObservableCollection<NavigationViewItemModel>();

            this._eventAggregator.GetEvent<AfterSaveDetailsEvent>()
                .Subscribe(AfterDetailsSaveEventHandler);

            this._eventAggregator.GetEvent<AfterDeleteEvent>()
                .Subscribe(AfterDeleteHandler);
        }

        private void AfterDeleteHandler(AfterDeleteEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    var friend = this.Friends
                        .FirstOrDefault(f => f.Id == args.Id);

                    if (friend != null)
                    {
                        Friends.Remove(friend);
                    }
                    break;

            }

        }

        private void AfterDetailsSaveEventHandler(AfterSaveDetailsEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    var friend = this.Friends
                .FirstOrDefault(f => f.Id == args.Id);

                    if (friend == null)
                    {
                        Friends.Add(
                            new NavigationViewItemModel(
                                args.Id,
                                args.DisplayProperty,
                                this._eventAggregator,
                                nameof(FriendDetailViewModel)));
                    }
                    else
                    {
                        friend.DisplayProperty = args.DisplayProperty;
                    }
                    break;
            }
        }

        public async Task LoadAsync()
        {
            var friendsLookupServiceCall = await this._friendService
                .GetAllAsync();

            Friends.Clear();

            foreach (var friend in friendsLookupServiceCall)
            {
                Friends.Add(
                    new NavigationViewItemModel(
                        friend.Id,
                        friend.FullName(),
                        this._eventAggregator,
                        nameof(FriendDetailViewModel)));
            }
        }
        public ObservableCollection<NavigationViewItemModel> Friends { get; set; }

    }
}
