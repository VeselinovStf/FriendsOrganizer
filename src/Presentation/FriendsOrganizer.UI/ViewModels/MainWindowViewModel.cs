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
using System.Collections.ObjectModel;
using System.Linq;
using FriendsOrganizer.UI.ModelsWrappers;

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

            this._eventAggregator.GetEvent<AfterDetailsCloseEvent>()
                .Subscribe(AfterDetailCloseEventHandler);

            this.DetailViewModels = new ObservableCollection<IDetailViewModel>();

            CreateNewDetailsCommmand = new DelegateCommand<Type>(OnCreateNewDetailsExecute);
        }

     

        private void AfterDetailCloseEventHandler(AfterDetailsCloseEventArgs args)
        {
            RemoveDetailMethod(args.Id, args.ViewModelName);
        }

        private void AfterDeleteHandler(AfterDeleteEventArgs args)
        {
            RemoveDetailMethod(args.Id, args.ViewModelName);

        }

  
        private void RemoveDetailMethod(int id, string viewModelName)
        {
            var detailsViewModel = DetailViewModels
                   .SingleOrDefault(e => e.Id == id &&
                   e.GetType().Name == viewModelName);

            if (detailsViewModel != null)
            {
                DetailViewModels.Remove(detailsViewModel);
            }
        }

        public ObservableCollection<IDetailViewModel> DetailViewModels { get; }

        private IDetailViewModel _selectedDetailViewModel;

        public IDetailViewModel SelectedDetailViewModel
        {
            get 
            { 
                return _selectedDetailViewModel;
            }
            set 
            {
                _selectedDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        

        public async Task LoadAsync()
        {
            await this.NavigationViewModel.LoadAsync();
        }

        private async void OnSelectedFriendEventHandler(OpenDetailEventArgs args)
        {
            var detailsViewModel = DetailViewModels
              .SingleOrDefault(e => e.Id == args.Id &&
              e.GetType().Name == args.ViewModelName);

            if (detailsViewModel == null)
            {
                detailsViewModel = this._detailViewModelCreator[args.ViewModelName];

                
                try
                {
                    if (args.Id == 0)
                    {
                        await detailsViewModel.LoadAddableAsync();
                    }
                    else
                    {
                        await detailsViewModel.LoadAsync(args.Id);

                    }

                    DetailViewModels.Add(detailsViewModel);
                }
                catch
                {
                    //Db Concurrency
                    this._messageDialogService.ShowInfoDialog("Could not load the entry, Maybe is was deleted in the mentime by another user");
                    await NavigationViewModel.LoadAsync();
                    return;
                }
               
            }

            SelectedDetailViewModel = detailsViewModel;    

          
        }


        private  void OnCreateNewDetailsExecute(Type viewModelType)
        {
            OnSelectedFriendEventHandler(
                new OpenDetailEventArgs { ViewModelName = viewModelType.Name});
        }

    }
}
