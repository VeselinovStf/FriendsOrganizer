using AutoMapper;
using FriendsOrganizer.Friends.Service.Abstraction;
using FriendsOrganizer.UI.Models;
using System.Threading.Tasks;

namespace FriendsOrganizer.UI.ViewModels
{
    public class FriendDetailViewModel : ViewModelBase
    {
        private readonly IFriendService _friendService;
        private readonly IMapper _mapper;

        public FriendDetailViewModel(
            IFriendService friendService,
            IMapper mapper)
        {
            this._friendService = friendService;
            this._mapper = mapper;
        }

        public async Task Load(int friendId)
        {
            var friendServiceCall = await this._friendService
                .GetAsync(friendId);

            Friend = this._mapper.Map<FriendModel>(friendServiceCall);
        }

        private FriendModel _friend;

        public FriendModel Friend
        {
            get { return _friend; }
            set 
            { 
                _friend = value;
                OnPropertyChanged();
            }
        }

    }
}
