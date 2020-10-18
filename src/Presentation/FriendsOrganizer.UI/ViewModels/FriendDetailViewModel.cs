using AutoMapper;
using FriendsOrganizer.Friends.Service.Abstraction;
using FriendsOrganizer.Friends.Service.DTOs;
using FriendsOrganizer.UI.Events;
using FriendsOrganizer.UI.Models;
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
        private readonly IMapper _mapper;
        private readonly IEventAggregator _eventAggregator;

        public FriendDetailViewModel(
            IFriendService friendService,
            IMapper mapper,
            IEventAggregator eventAggregator)
        {
            this._friendService = friendService;
            this._mapper = mapper;
            this._eventAggregator = eventAggregator;

            this._eventAggregator.GetEvent<OpenFriendDetailsEvent>()
                .Subscribe(OnSelectedFriendEventHandler);

            this.SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private bool OnSaveCanExecute()
        {
            //TODO: Validation
            return Friend!= null && !Friend.HasErrors;
        }

        private async void OnSaveExecute()
        {
            var updateModel = this._mapper.Map<FriendDTO>(this.Friend.Model);

            await this._friendService
                 .UpdateFriendAsync(updateModel);

            this._eventAggregator.GetEvent<AfterFriendSaveDetailsEvent>()
                .Publish(new AfterFriendSaveDetailsLookup()
                {
                    Id = updateModel.Id,
                    DisplayProperty = updateModel.FirstName + " " + updateModel.LastName
                });
        }

        private async void OnSelectedFriendEventHandler(int friendId)
        {
            await this.Load(friendId);
        }

        public async Task Load(int friendId)
        {
            var friendServiceCall = await this._friendService
                .GetAsync(friendId);

            var friend = this._mapper.Map<FriendModel>(friendServiceCall);

            Friend = new FriendModelWrapper(friend);

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
