using FriendsOrganizer.UI.Events;
using FriendsOrganizer.UI.Models;
using Prism.Events;
using System;
using System.Threading.Tasks;
using FriendsOrganizer.UI.UIServices;
using Prism.Commands;
using FriendsOrganizer.UI.ViewModels.Abstraction;

namespace FriendsOrganizer.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;

        public DelegateCommand CreateNewFriendCommmand { get; }
        public Func<IFriendDetailViewModel> _detailViewModelCreator { get; }

        public NavigationViewModel NavigationViewModel { get; }

        public MainWindowViewModel(
            NavigationViewModel navigationViewModel,
            Func<IFriendDetailViewModel> friendDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            this.NavigationViewModel = navigationViewModel;
            this._detailViewModelCreator = friendDetailViewModelCreator;
            this._eventAggregator = eventAggregator;
            this._messageDialogService = messageDialogService;

            this._eventAggregator.GetEvent<OpenFriendDetailsEvent>()
               .Subscribe(OnSelectedFriendEventHandler);

            this._eventAggregator.GetEvent<AfterFriendDeleteEvent>()
                .Subscribe(AfterFriendDeleteHandler);

            CreateNewFriendCommmand = new DelegateCommand(OnCreateNewFriendExecute);
        }

        private void AfterFriendDeleteHandler(int obj)
        {
            this.DetailViewModel = null;
            
        }

        private IDetailViewModel _detailViewModel;

        public IDetailViewModel DetailViewModel
        {
            get 
            { 
                return _detailViewModel;
            }
            private set 
            {
                _detailViewModel = value;
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
           
            DetailViewModel = this._detailViewModelCreator();

            await this.DetailViewModel.LoadAsync(friendId);
        }

        private void MessageUserIfFriendIsSelected()
        {
            if (DetailViewModel != null && DetailViewModel.HasChange)
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

            DetailViewModel = this._detailViewModelCreator();

            await this.DetailViewModel.LoadAddableAsync();
        }

    }
}
