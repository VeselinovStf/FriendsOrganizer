using FriendsOrganizer.UI.Events;
using FriendsOrganizer.UI.Models;
using Prism.Events;
using System;
using System.Threading.Tasks;
using FriendsOrganizer.UI.UIServices;
using Prism.Commands;

namespace FriendsOrganizer.UI.ViewModels
{
    public class MainWindowViewModel :ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;

        public DelegateCommand CreateNewFriendCommmand { get; }
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

            CreateNewFriendCommmand = new DelegateCommand(OnCreateNewFriendExecute);
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
            MessageUserIfFriendIsSelected();
           
            FriendDetailViewModel = this._friendDetailViewModelCreator();

            await this.FriendDetailViewModel.LoadAsync(friendId);
        }

        private void MessageUserIfFriendIsSelected()
        {
            if (FriendDetailViewModel != null && FriendDetailViewModel.HasChange)
            {
                var result = this._messageDialogService.ShowOkCancelDialog("Are you shore to change friend? Your changes will be lost!", "Question");

                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
        }

        private async void OnCreateNewFriendExecute()
        {
            MessageUserIfFriendIsSelected();

            FriendDetailViewModel = this._friendDetailViewModelCreator();

            await this.FriendDetailViewModel.LoadAddableAsync();
        }

    }
}
