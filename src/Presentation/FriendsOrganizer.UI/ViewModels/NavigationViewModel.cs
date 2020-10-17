using AutoMapper;
using FriendsOrganizer.Friends.Service.Abstraction;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FriendsOrganizer.UI.Models
{
    public class NavigationViewModel
    {
        private readonly IFriendService _friendService;
        private readonly IMapper _mapper;

        public NavigationViewModel(
            IFriendService friendService,
            IMapper mapper)
        {
            this._friendService = friendService;
            this._mapper = mapper;
            this.Friends = new ObservableCollection<LookupItem>();
        }

        public async Task LoadAsync()
        {
            var friendsLookupServiceCall = await this._friendService
                .GetAllFriendsLookupAsync();

            Friends.Clear();

            var friendsLookup = this._mapper.Map<List<LookupItem>>(friendsLookupServiceCall);

            foreach (var friend in friendsLookup)
            {
                Friends.Add(friend);
            }
        }
        public ObservableCollection<LookupItem> Friends { get; set; }
    }
}
