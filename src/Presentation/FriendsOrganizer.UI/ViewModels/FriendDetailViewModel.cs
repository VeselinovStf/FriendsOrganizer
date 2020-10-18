using AutoMapper;
using FriendsOrganizer.Friends.Service.Abstraction;
using FriendsOrganizer.Friends.Service.DTOs;
using FriendsOrganizer.UI.Events;
using FriendsOrganizer.UI.Models;
using Prism.Commands;
using Prism.Events;
using System;
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
            return true;
        }

        private async void OnSaveExecute()
        {
            var updateModel = this._mapper.Map<FriendDTO>(this.Friend);

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

        public ICommand SaveCommand { get; }

    }
}
