using FriendOrganizer.Meetings.Service.Abstraction;
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
        private readonly IMeetingService _meetingService;

        public NavigationViewModel(
            IFriendService friendService,
            IEventAggregator eventAggregator,
            IMeetingService meetingService)
        {
            this._friendService = friendService;
            this._eventAggregator = eventAggregator;
            this._meetingService = meetingService;
            this.Friends = new ObservableCollection<NavigationViewItemModel>();
            this.Meetings = new ObservableCollection<NavigationViewItemModel>();

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
                    AfterDetailsDeleted(Friends, args);
                    break;
                case nameof(MeetingDetailViewModel):
                    AfterDetailsDeleted(Meetings, args);
                    break;
            }

        }

        private void AfterDetailsDeleted(ObservableCollection<NavigationViewItemModel> items, AfterDeleteEventArgs args)
        {
            var item = items
                       .FirstOrDefault(f => f.Id == args.Id);

            if (item != null)
            {
                items.Remove(item);
            }
        }

        private void AfterDetailsSaveEventHandler(AfterSaveDetailsEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    AfterDetailsSaved(Friends, args);
                    break;
                case nameof(MeetingDetailViewModel):
                    AfterDetailsSaved(Meetings, args);
                    break;
            }
        }

        private void AfterDetailsSaved(ObservableCollection<NavigationViewItemModel> items, AfterSaveDetailsEventArgs args)
        {
            var item = items
                    .FirstOrDefault(f => f.Id == args.Id);

            if (item == null)
            {
                items.Add(
                    new NavigationViewItemModel(
                        args.Id,
                        args.DisplayProperty,
                        this._eventAggregator,
                        nameof(args.ViewModelName)));
            }
            else
            {
                item.DisplayProperty = args.DisplayProperty;
            }
        }

        public async Task LoadAsync()
        {
            await LoadNavigationFriends();
            await LoadNavigationMeetings();        
        }

        private async Task LoadNavigationMeetings()
        {
            var meetingsLookupServiceCall = await this._meetingService
             .GetAllAsync();

            Meetings.Clear();

            foreach (var meeting in meetingsLookupServiceCall)
            {
                Meetings.Add(
                    new NavigationViewItemModel(
                        meeting.Id,
                        meeting.Title,
                        this._eventAggregator,
                        nameof(MeetingDetailViewModel)));
            }
        }

        private async Task LoadNavigationFriends()
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
        public ObservableCollection<NavigationViewItemModel> Meetings { get; set; }

    }
}
