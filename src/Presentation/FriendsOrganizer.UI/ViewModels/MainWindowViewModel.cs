using FriendsOrganizer.UI.Events;
using FriendsOrganizer.UI.Models;
using Prism.Events;
using System;
using System.Threading.Tasks;
using FriendsOrganizer.UI.UIServices;
using Prism.Commands;
using FriendsOrganizer.UI.ViewModels.Abstraction;
using FriendsOrganizer.UI.Events.Arguments;
using Autofac.Features.Indexed;

namespace FriendsOrganizer.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IIndex<string, IDetailViewModel> _detailViewModelCreator;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;

        public DelegateCommand<Type> CreateNewDetailsCommmand { get; }


        public NavigationViewModel NavigationViewModel { get; }

        public MainWindowViewModel(
            NavigationViewModel navigationViewModel,
            IIndex<string, IDetailViewModel> detailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            this.NavigationViewModel = navigationViewModel;
            this._detailViewModelCreator = detailViewModelCreator;
            this._eventAggregator = eventAggregator;
            this._messageDialogService = messageDialogService;

            this._eventAggregator.GetEvent<OpenDetailsEvent>()
               .Subscribe(OnSelectedFriendEventHandler);

            this._eventAggregator.GetEvent<AfterDeleteEvent>()
                .Subscribe(AfterDeleteHandler);

            CreateNewDetailsCommmand = new DelegateCommand<Type>(OnCreateNewDetailsExecute);
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
            MessageUserIfIsSelected();

            DetailViewModel = this._detailViewModelCreator[args.ViewModelName];           

            await this.DetailViewModel.LoadAsync(args.Id);
        }

        private void MessageUserIfIsSelected()
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

        private async void OnCreateNewDetailsExecute(Type viewModelType)
        {
            MessageUserIfIsSelected();

            DetailViewModel = this._detailViewModelCreator[viewModelType.Name];

            await this.DetailViewModel.LoadAddableAsync();
        }

    }
}
