using FriendsOrganizer.UI.Events;
using FriendsOrganizer.UI.Models;
using Prism.Events;
using System;
using System.Threading.Tasks;
using FriendsOrganizer.UI.UIServices;

namespace FriendsOrganizer.UI.ViewModels
{
    public class MainWindowViewModel :ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;

        public Func<FriendDetailViewModel> _friendDetailViewModelCreator { get; }

        public NavigationViewModel NavigationViewModel { get; }

        public MainWindowViewModel(
            NavigationViewModel navigationViewModel,
            Func<FriendDetailViewModel> friendDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            this.NavigationViewModel = navigationViewModel;
            this._friendDetailViewModelCreator = friendDetailViewModelCreator;
            this._eventAggregator = eventAggregator;
            this._messageDialogService = messageDialogService;

            this._eventAggregator.GetEvent<OpenFriendDetailsEvent>()
               .Subscribe(OnSelectedFriendEventHandler);
        }

        private FriendDetailViewModel _friendDetailViewModel;

        public FriendDetailViewModel FriendDetailViewModel
        {
            get 
            { 
                return _friendDetailViewModel;
            }
            private set 
            {
                _friendDetailViewModel = value;
                OnPropertyChanged();
            }
        }


        public async Task LoadAsync()
        {
            await this.NavigationViewModel.LoadAsync();
        }

        private async void OnSelectedFriendEventHandler(int friendId)
        {
            if (FriendDetailViewModel != null && FriendDetailViewModel.HasChange)
            {
                var result = this._messageDialogService.ShowOkCancelDialog("Are you shore to change friend? Your changes will be lost!", "Question");

                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            FriendDetailViewModel = this._friendDetailViewModelCreator();

            await this.FriendDetailViewModel.Load(friendId);
        }
    }
}
