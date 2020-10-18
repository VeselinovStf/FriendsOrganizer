using AutoMapper;
using FriendsOrganizer.Friends.Service.Abstraction;
using FriendsOrganizer.UI.Events;
using FriendsOrganizer.UI.ViewModels;
using Prism.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FriendsOrganizer.UI.Models
{
    public class NavigationViewModel  : ViewModelBase
    {
        private readonly IFriendService _friendService;
        private readonly IMapper _mapper;
        private readonly IEventAggregator _eventAggregator;

        public NavigationViewModel(
            IFriendService friendService,
            IMapper mapper,
            IEventAggregator eventAggregator)
        {
            this._friendService = friendService;
            this._mapper = mapper;
            this._eventAggregator = eventAggregator;
            this.Friends = new ObservableCollection<LookupItem>();
        }

        public async Task LoadAsync()
        {
            var friendsLookupServiceCall = await this._friendService
                .GetAllAsync();

            Friends.Clear();

            var friendsLookup = this._mapper.Map<List<LookupItem>>(friendsLookupServiceCall);

            foreach (var friend in friendsLookup)
            {
                Friends.Add(friend);
            }
        }
        public ObservableCollection<LookupItem> Friends { get; set; }

        private LookupItem _selectedFriend;

        public LookupItem SelectedFriend
        {
            get { return _selectedFriend; }
            set 
            { 
                _selectedFriend = value;
                OnPropertyChanged();
                if (_selectedFriend != null)
                {
                    this._eventAggregator.GetEvent<OpenFriendDetailsEvent>()
                        .Publish(this._selectedFriend.Id);
                }
            }
        }

    }
}
