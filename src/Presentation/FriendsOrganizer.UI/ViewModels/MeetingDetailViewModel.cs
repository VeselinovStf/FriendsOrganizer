using FriendOrganizer.Meetings.Service.Abstraction;
using FriendsOrganizer.Data.Models;
using FriendsOrganizer.Friends.Service.Abstraction;
using FriendsOrganizer.UI.Models;
using FriendsOrganizer.UI.ModelsWrappers;
using FriendsOrganizer.UI.UIServices;
using FriendsOrganizer.UI.ViewModels.Abstraction;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendsOrganizer.UI.ViewModels
{
    public class MeetingDetailViewModel : DetailViewModelBase, IMeetingDetailViewModel
    {
        private readonly IMeetingService _meetingService;
        private readonly IFriendService _friendService;
        private readonly IMessageDialogService _messageDialogService;
        private FriendModel _selectedAddedFried;
        private FriendModel _selectedAvailibleFried;
        private MeetingModelWrapper _meeting;
        private IEnumerable<Friend> _allFriends;

        public ObservableCollection<FriendModel> AddedFriends { get; }
        public ObservableCollection<FriendModel> AvailibleFriends { get; }

        public ICommand AddFriendCommand { get; }
        public ICommand RemoveFriendCommand { get; }

        public MeetingDetailViewModel(
            IMeetingService meetingService,
            IFriendService friendService,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService) : base(eventAggregator)
        {
            this._meetingService = meetingService;
            this._friendService = friendService;
            this._messageDialogService = messageDialogService;
            this.AddedFriends = new ObservableCollection<FriendModel>();
            this.AvailibleFriends = new ObservableCollection<FriendModel>();

            this.AddFriendCommand = new DelegateCommand(OnAddFriendExecute, OnAddFriendCanExecute);
            this.RemoveFriendCommand = new DelegateCommand(OnRemoveFriendExecute, OnRemoveFriendCanExecute);
        }

        private bool OnRemoveFriendCanExecute()
        {
            return this.SelectedAddedFried != null;
        }

        private void OnRemoveFriendExecute()
        {
            var selectedFriendToRemove = SelectedAddedFried;

            var friendToRemove = Meeting.Model.FriendMeetings
                .FirstOrDefault(e => e.FriendId == selectedFriendToRemove.Id && e.MeetingId == Meeting.Id);

            Meeting.Model.FriendMeetings.Remove(friendToRemove);

            AddedFriends.Remove(selectedFriendToRemove);
            AvailibleFriends.Add(selectedFriendToRemove);
            HasChange = _meetingService.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private bool OnAddFriendCanExecute()
        {
            return this.SelectedAvailibleFried != null;
        }

        private void OnAddFriendExecute()
        {
            var friendToAdd = SelectedAvailibleFried;

            Meeting.Model.FriendMeetings
                .Add(new FriendMeeting() { FriendId = friendToAdd.Id, MeetingId = Meeting.Id });

            AddedFriends.Add(friendToAdd);
            AvailibleFriends.Remove(friendToAdd);
            HasChange = _meetingService.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                
        }

        public MeetingModelWrapper Meeting
        {
            get
            {
                return _meeting;
            }
            set
            {
                _meeting = value;
                OnPropertyChanged();
            }
        }



        public FriendModel SelectedAddedFried
        {
            get { return _selectedAddedFried; }
            set
            {
                _selectedAddedFried = value;
                OnPropertyChanged();
                ((DelegateCommand)RemoveFriendCommand).RaiseCanExecuteChanged();
            }
        }



        public FriendModel SelectedAvailibleFried
        {
            get { return _selectedAvailibleFried; }
            set
            {
                _selectedAvailibleFried = value;
                OnPropertyChanged();
                ((DelegateCommand)AddFriendCommand).RaiseCanExecuteChanged();
            }
        }


        public override async Task LoadAddableAsync()
        {
            var newMeeting = await CreateMeeting();

            InitializeMeeting(newMeeting);



        }

        private async Task InitializePickList()
        {
            _allFriends = await this._friendService
                .GetAllAsync();

            SetupPickList();
        }

        private void SetupPickList()
        {
            var meetingFriendIds = Meeting
                .Model
                .FriendMeetings
                .Select(fm => fm.Friend)
                .Select(f => f.Id).ToList();

            var addableFriends = _allFriends
                .Where(f => meetingFriendIds.Contains(f.Id))
                .OrderBy(f => f.FirstName);

            var availibleFriends = _allFriends
                .Except(addableFriends)
                .OrderBy(f => f.FirstName);

            AddedFriends.Clear();
            AvailibleFriends.Clear();

            foreach (var af in addableFriends)
            {
                AddedFriends.Add(new FriendModel()
                {
                    Id = af.Id,
                    Email = af.Email,
                    FirstName = af.FirstName,
                    LastName = af.LastName
                });
            }

            foreach (var af in availibleFriends)
            {
                AvailibleFriends.Add(new FriendModel()
                {
                    Id = af.Id,
                    Email = af.Email,
                    FirstName = af.FirstName,
                    LastName = af.LastName
                });
            }
        }

        private async Task<Meeting> CreateMeeting()
        {
            var newMeeting = new Meeting()
            {
                StartAt = DateTime.Now.Date,
                EndAt = DateTime.Now.Date,
            };

            await this._meetingService.AddAsync(newMeeting);
            return newMeeting;
        }

        public override async Task LoadAsync(int id)
        {
            var meetingServiceCall = await this._meetingService
               .GetAsync(id);

           
            InitializeMeeting(meetingServiceCall);
            Id = Meeting.Id;
            await InitializePickList();
        }

        private void InitializeMeeting(Data.Models.Meeting meetingServiceCall)
        {
            Meeting = new MeetingModelWrapper(meetingServiceCall);

            Meeting.PropertyChanged += (s, e) =>
            {
                if (!HasChange)
                {
                    HasChange = this._meetingService.HasChanges();
                }
                if (e.PropertyName == nameof(Meeting.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }

            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            if (Meeting.Id == 0)
            {
                Meeting.Title = "";
            }
        }

        protected override async void OnDeleteExecute()
        {
            var result = this._messageDialogService
                .ShowOkCancelDialog("Do you really want to delete this meeting?", "Delete Meeting");

            if (result == MessageDialogResult.Ok)
            {
                this._meetingService.Remove(Meeting.Model);
                await this._meetingService.SaveChangesAsync();
                RaiseDetailDeleteEvent(Meeting.Id);
            }
        }

        protected override bool OnSaveCanExecute()
        {
            return Meeting != null && !Meeting.HasErrors && HasChange;
        }

        protected override async void OnSaveExecute()
        {
            await this._meetingService.SaveChangesAsync();
            HasChange = this._meetingService.HasChanges();
            Id = Meeting.Id;
            RaiseDetailSaveEvent(Meeting.Id, Meeting.Title);
        }
    }
}
