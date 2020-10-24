using FriendsOrganizer.Friends.Service.Abstraction;
using FriendsOrganizer.ProgrammingLanguages.Service.Abstraction;
using FriendsOrganizer.UI.Events;
using FriendsOrganizer.UI.ModelsWrappers;
using FriendsOrganizer.UI.UIServices;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendsOrganizer.UI.ViewModels
{
    public class FriendDetailViewModel : ViewModelBase
    {
        private readonly IFriendService _friendService;      
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IProgrammingLanguagesService _programmingLanguagesService;
        public ObservableCollection<ProgrammingLanguageModelWrapper> ProgrammingLanguages { get; set; }

        public FriendDetailViewModel(
            IFriendService friendService,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IProgrammingLanguagesService programmingLanguagesService)
        {
            this._friendService = friendService;
            this._eventAggregator = eventAggregator;
            this._messageDialogService = messageDialogService;
            this._programmingLanguagesService = programmingLanguagesService;

            this.SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            this.DeleteCommand = new DelegateCommand(OnDeleteExecute);

            this.ProgrammingLanguages = new ObservableCollection<ProgrammingLanguageModelWrapper>();
        }

        private async void OnDeleteExecute()
        {
            var confirmDeleteMessage = this._messageDialogService
               .ShowOkCancelDialog("Are you really want to delete this friend?", "Delete friend");

            if (confirmDeleteMessage == MessageDialogResult.Ok)
            {
                await this._friendService.RemoveAsync(Friend.Model);

                this._eventAggregator.GetEvent<AfterFriendDeleteEvent>()
                    .Publish(Friend.Model.Id);
            }
           
        }

        private bool OnSaveCanExecute()
        {
            return Friend!= null && !Friend.HasErrors && HasChange;
        }

        private async void OnSaveExecute()
        {
            await this._friendService
                 .UpdateFriendAsync();

            HasChange = this._friendService.HasChanges();

            this._eventAggregator.GetEvent<AfterFriendSaveDetailsEvent>()
                .Publish(new AfterFriendSaveDetailsLookup()
                {
                    Id = Friend.Id,
                    DisplayProperty = Friend.FirstName + " " + Friend.LastName
                });
        }



        public async Task LoadAsync(int friendId)
        {
            var friendServiceCall = await this._friendService
                .GetAsync(friendId);

            Friend = new FriendModelWrapper(friendServiceCall);

            CheckChangeHandler(Friend);

            LoadProgrammyngLanguages();
           
        }

        private async void LoadProgrammyngLanguages()
        {
            ProgrammingLanguages.Clear();
          
            var languages = await this._programmingLanguagesService
                .GetAllAsync();

            foreach (var l in languages)
            {
                this.ProgrammingLanguages.Add(
                    new ProgrammingLanguageModelWrapper(l)
                    );
            }
        }

        private void CheckChangeHandler(FriendModelWrapper friend)
        {
            Friend.PropertyChanged += (s, e) =>
            {
                if (!HasChange)
                {
                    HasChange = this._friendService.HasChanges();
                }
                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }

            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        public async Task LoadAddableAsync()
        {
            var friendServiceCall = await this._friendService
                .AddNewAsync();

            Friend = new FriendModelWrapper(friendServiceCall);

            CheckChangeHandler(Friend);

            if (Friend.Id == 0)
            {
                TriggerValidation(Friend);               
            }
        }

        private void TriggerValidation(FriendModelWrapper friend)
        {
            Friend.FirstName = "";
            Friend.LastName = "";
            Friend.Email = "";
        }

        private bool _hasChanges;

        public bool HasChange
        {
            get { return _hasChanges; }
            set 
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
               
            }
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

        public ICommand DeleteCommand { get; }
        
    }
}
