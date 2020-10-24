using FriendsOrganizer.Data.Models;
using FriendsOrganizer.Friends.Service.Abstraction;
using FriendsOrganizer.ProgrammingLanguages.Service.Abstraction;
using FriendsOrganizer.UI.Events;
using FriendsOrganizer.UI.ModelsWrappers;
using FriendsOrganizer.UI.UIServices;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        public ObservableCollection<FriendPhoneModelWrapper> PhoneNumbers { get; set; }

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
            this.PhoneNumbers = new ObservableCollection<FriendPhoneModelWrapper>();

            this.SavePhoneNumberCommand = new DelegateCommand(OnSavePhoneNumberExecute);
            this.DeletePhoneNumberCommand = new DelegateCommand(OnDeletePhoneNumberExecute, OnDeletePhoneNumberCanExecute);
        }

        private void OnDeletePhoneNumberExecute()
        {
            //throw new NotImplementedException();
        }

        private bool OnDeletePhoneNumberCanExecute()
        {
            return this.SelectedPhoneNumber != null;
        }

        private void OnSavePhoneNumberExecute()
        {
            //throw new NotImplementedException();
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
            return Friend!= null && 
                PhoneNumbers.All(p => !p.HasErrors) &&
                !Friend.HasErrors && 
                HasChange;
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

            InitializePhoneNumbers(Friend.PhoneNumbers);

            LoadProgrammyngLanguages();
           
        }

        private void InitializePhoneNumbers(ICollection<FriendPhoneNumber> phoneNumbers)
        {
            foreach (var wrapper in PhoneNumbers)
            {
                wrapper.PropertyChanged -= FriendPhoneModelWrapper_PropertyChanged;
            }

            PhoneNumbers.Clear();

            foreach (var phoneNumber in phoneNumbers)
            {
                var wrapper = new FriendPhoneModelWrapper(phoneNumber);
                PhoneNumbers.Add(wrapper);
                wrapper.PropertyChanged += FriendPhoneModelWrapper_PropertyChanged;
                
            }
        }

        private void FriendPhoneModelWrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChange)
            {
                HasChange = this._friendService
                    .HasChanges();
            }

            if (e.PropertyName == nameof(FriendPhoneModelWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
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

        private FriendPhoneModelWrapper _selectedPhoneNumber;

        public FriendPhoneModelWrapper SelectedPhoneNumber
        {
            get { return _selectedPhoneNumber; }
            set 
            { 
                _selectedPhoneNumber = value;
                OnPropertyChanged();
                //When this property is set -> The can be changed/removed etc.
                ((DelegateCommand)DeletePhoneNumberCommand).RaiseCanExecuteChanged();
            }
        }


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

        public ICommand SavePhoneNumberCommand { get; set; }
        public ICommand DeletePhoneNumberCommand { get; set; }

    }
}
