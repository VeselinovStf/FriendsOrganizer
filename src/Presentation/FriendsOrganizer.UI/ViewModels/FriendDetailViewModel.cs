using FriendsOrganizer.Data.Models;
using FriendsOrganizer.Friends.Service.Abstraction;
using FriendsOrganizer.ProgrammingLanguages.Service.Abstraction;
using FriendsOrganizer.UI.Events;
using FriendsOrganizer.UI.Events.Arguments;
using FriendsOrganizer.UI.ModelsWrappers;
using FriendsOrganizer.UI.UIServices;
using FriendsOrganizer.UI.ViewModels.Abstraction;
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
    public class FriendDetailViewModel : DetailViewModelBase, IFriendDetailViewModel
    {
        private FriendModelWrapper _friend;
        private FriendPhoneModelWrapper _selectedPhoneNumber;
        private readonly IFriendService _friendService;
        private readonly IProgrammingLanguagesService _programmingLanguagesService;
        public ObservableCollection<ProgrammingLanguageModelWrapper> ProgrammingLanguages { get; set; }
        public ObservableCollection<FriendPhoneModelWrapper> PhoneNumbers { get; set; }

        public FriendDetailViewModel(
            IFriendService friendService,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IProgrammingLanguagesService programmingLanguagesService)
            : base(eventAggregator,messageDialogService)
        {
            this._friendService = friendService;
            this._programmingLanguagesService = programmingLanguagesService;

            this._eventAggregator.GetEvent<AfterCollectionSaveEvent>()
                .Subscribe(AfterCollectionSaved);

            this.ProgrammingLanguages = new ObservableCollection<ProgrammingLanguageModelWrapper>();
            this.PhoneNumbers = new ObservableCollection<FriendPhoneModelWrapper>();

            this.SavePhoneNumberCommand = new DelegateCommand(OnSavePhoneNumberExecute);
            this.DeletePhoneNumberCommand = new DelegateCommand(OnDeletePhoneNumberExecute, OnDeletePhoneNumberCanExecute);
        }

        private  void AfterCollectionSaved(AfterCollectionSaveEventArgs args)
        {
            if (args.ViewModelName == nameof(ProgrammingLanguageDetailsViewModel))
            {
                 LoadProgrammyngLanguages();
            }
        }

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

        public FriendModelWrapper Friend
        {
            get { return _friend; }
            set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }

        public ICommand SavePhoneNumberCommand { get; set; }
        public ICommand DeletePhoneNumberCommand { get; set; }

        private void OnDeletePhoneNumberExecute()
        {
            SelectedPhoneNumber.PropertyChanged -= FriendPhoneModelWrapper_PropertyChanged;
            Friend.PhoneNumbers.Remove(SelectedPhoneNumber.Model);
            PhoneNumbers.Remove(SelectedPhoneNumber);
            SelectedPhoneNumber = null;
            HasChange = this._friendService
                .HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private bool OnDeletePhoneNumberCanExecute()
        {
            return this.SelectedPhoneNumber != null;
        }

        private void OnSavePhoneNumberExecute()
        {
            var newNumber = new FriendPhoneModelWrapper(new FriendPhoneNumber());
            newNumber.PropertyChanged += FriendPhoneModelWrapper_PropertyChanged;

            PhoneNumbers.Add(newNumber);

            Friend.PhoneNumbers.Add(newNumber.Model);
            newNumber.PhoneNumber = "";

        }

        protected override async void OnDeleteExecute()
        {
            if (await this._friendService.HasMeetingAsync(Friend.Id))
            {
                base._messageDialogService.ShowInfoDialog($"{Friend.FirstName} {Friend.LastName} can't be deleted, its participating in Meeting.");
                return;

            }

            var confirmDeleteMessage = base._messageDialogService
               .ShowOkCancelDialog("Are you really want to delete this friend?", "Delete friend");

            if (confirmDeleteMessage == MessageDialogResult.Ok)
            {
                await this._friendService.RemoveAsync(Friend.Model);

                RaiseDetailDeleteEvent(Friend.Model.Id);
            }

        }

        protected override bool OnSaveCanExecute()
        {
            return Friend != null &&
                PhoneNumbers.All(p => !p.HasErrors) &&
                !Friend.HasErrors &&
                HasChange;
        }

        protected override async void OnSaveExecute()
        {
            await this._friendService
                 .UpdateFriendAsync();

            HasChange = this._friendService.HasChanges();
            Id = Friend.Id;
            RaiseDetailSaveEvent(Friend.Id, Friend.FirstName + " " + Friend.LastName);

        }

        public override async Task LoadAsync(int friendId)
        {
            var friendServiceCall = await this._friendService
                .GetAsync(friendId);

            Friend = new FriendModelWrapper(friendServiceCall);

            Id = Friend.Id;

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

        private void SetTitle()
        {
            Title = Friend.FirstName + " " + Friend.LastName;
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
                if (e.PropertyName == nameof(Friend.FirstName) || e.PropertyName == nameof(Friend.LastName))
                {
                    SetTitle();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if (Friend.Id == 0)
            {
                Friend.FirstName = "";
            }
            SetTitle();
        }

        public override async Task LoadAddableAsync()
        {
            var friendServiceCall = await this._friendService
                .AddNewAsync();

            Friend = new FriendModelWrapper(friendServiceCall);

            LoadProgrammyngLanguages();

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
    }
}
