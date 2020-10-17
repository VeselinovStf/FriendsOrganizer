using AutoMapper;
using FriendsOrganizer.Friends.Service.Abstraction;
using FriendsOrganizer.Friends.Service.DTOs;
using FriendsOrganizer.UI.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FriendsOrganizer.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IFriendService _friendService;
        private readonly IMapper _mapper;
        private FriendModel _selectedFriend;

        public MainWindowViewModel(
            IFriendService friendService,
            IMapper mapper)
        {
            this.Friends = new ObservableCollection<FriendModel>();
            this._friendService = friendService;
            this._mapper = mapper;
        }

        public ObservableCollection<FriendModel> Friends { get; set; }

        public void Load()
        {
            var friendsDbServiceCall = this._friendService
                .GetAll()
                .ToList();

            Friends.Clear();

            var friendDbServiceCallModel = this._mapper
                .Map<IList<FriendModel>>(friendsDbServiceCall);

            foreach (var friend in friendDbServiceCallModel)
            {
                this.Friends.Add(friend);
            }
        }

        public FriendModel SelectedFriend
        {
            get { return _selectedFriend; }
            set 
            { 
                _selectedFriend = value;
                OnPropertyChanged();
            }
        }

    }
}
