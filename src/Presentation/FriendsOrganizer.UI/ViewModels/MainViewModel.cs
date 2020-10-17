using FriendsOrganizer.Friends.Service.Abstraction;
using FriendsOrganizer.UI.Models;
using System.Collections.ObjectModel;

namespace FriendsOrganizer.UI.ViewModels
{
    public class MainViewModel
    {
        private readonly IFriendService _friendService;
        private FriendModel _selectedFriend;

        public MainViewModel(IFriendService friendService)
        {
            this.Friends = new ObservableCollection<FriendModel>();
            this._friendService = friendService;
        }

        public ObservableCollection<FriendModel> Friends { get; set; }

        public void Load()
        {
            var friendsDbServiceCall = this._friendService
                .GetAll();

            Friends.Clear();

            foreach (var friend in friendsDbServiceCall)
            {
                this.Friends.Add(new FriendModel()
                {
                    FirstName = friend.FirstName,
                    LastName = friend.LastName,
                    Email = friend.Email
                });
            }
        }

        public FriendModel SelectedFriend
        {
            get { return _selectedFriend; }
            set { _selectedFriend = value; }
        }

    }
}
