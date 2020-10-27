using FriendsOrganizer.UI.Events;
using FriendsOrganizer.UI.Models;
using Prism.Events;
using System;
using System.Threading.Tasks;
using FriendsOrganizer.UI.UIServices;
using Prism.Commands;
using FriendsOrganizer.UI.ViewModels.Abstraction;
using FriendsOrganizer.UI.Events.Arguments;

namespace FriendsOrganizer.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly Func<IMeetingDetailViewModel> _meetingDetailViewModelCreator;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;

        public DelegateCommand CreateNewFriendCommmand { get; }
        public Func<IFriendDetailViewModel> _detailViewModelCreator { get; }

        public NavigationViewModel NavigationViewModel { get; }

        public MainWindowViewModel(
            NavigationViewModel navigationViewModel,
            Func<IFriendDetailViewModel> friendDetailViewModelCreator,
            Func<IMeetingDetailViewModel> meetingDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            this.NavigationViewModel = navigationViewModel;
            this._detailViewModelCreator = friendDetailViewModelCreator;
            this._meetingDetailViewModelCreator = meetingDetailViewModelCreator;
            this._eventAggregator = eventAggregator;
            this._messageDialogService = messageDialogService;

            this._eventAggregator.GetEvent<OpenDetailsEvent>()
               .Subscribe(OnSelectedFriendEventHandler);

            this._eventAggregator.GetEvent<AfterDeleteEvent>()
                .Subscribe(AfterDeleteHandler);

            CreateNewFriendCommmand = new DelegateCommand(OnCreateNewFriendExecute);
        }

        private void AfterDeleteHandler(AfterDeleteEventArgs args)
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

        private async void OnSelectedFriendEventHandler(OpenDetailEventArgs args)
        {
            MessageUserIfFriendIsSelected();

            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    DetailViewModel = this._detailViewModelCreator();
                    break;
                case nameof(MeetingDetailViewModel):
                    DetailViewModel = this._meetingDetailViewModelCreator();
                    break;
                default:
                    throw new Exception("Un recognized view");
            }
           

            await this.DetailViewModel.LoadAsync(args.Id);
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
