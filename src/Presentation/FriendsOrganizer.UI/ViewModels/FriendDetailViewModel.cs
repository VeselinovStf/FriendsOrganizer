using FriendsOrganizer.Friends.Service.Abstraction;
using FriendsOrganizer.UI.Events;
using FriendsOrganizer.UI.ModelsWrappers;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendsOrganizer.UI.ViewModels
{
    public class FriendDetailViewModel : ViewModelBase
    {
        private readonly IFriendService _friendService;      
        private readonly IEventAggregator _eventAggregator;

        public FriendDetailViewModel(
            IFriendService friendService,
            IEventAggregator eventAggregator)
        {
            this._friendService = friendService;
            this._eventAggregator = eventAggregator;

            this.SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private bool OnSaveCanExecute()
        {
            //TODO: Validation
            return Friend!= null && !Friend.HasErrors;
        }

        private async void OnSaveExecute()
        {
            await this._friendService
                 .UpdateFriendAsync();

            this._eventAggregator.GetEvent<AfterFriendSaveDetailsEvent>()
                .Publish(new AfterFriendSaveDetailsLookup()
                {
                    Id = Friend.Id,
                    DisplayProperty = Friend.FirstName + " " + Friend.LastName
                });
        }



        public async Task Load(int friendId)
        {
            var friendServiceCall = await this._friendService
                .GetAsync(friendId);

            Friend = new FriendModelWrapper(friendServiceCall);

            Friend.PropertyChanged += (s, e) =>
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private FriendModelWrapper _friend;

        public FriendModelWrapper Friend
        {
            get { return _friend; }
            set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

    }
}
