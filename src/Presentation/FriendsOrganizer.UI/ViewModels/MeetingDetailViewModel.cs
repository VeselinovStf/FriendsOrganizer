using FriendOrganizer.Meetings.Service.Abstraction;
using FriendsOrganizer.Data.Models;
using FriendsOrganizer.UI.ModelsWrappers;
using FriendsOrganizer.UI.UIServices;
using FriendsOrganizer.UI.ViewModels.Abstraction;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace FriendsOrganizer.UI.ViewModels
{
    public class MeetingDetailViewModel : DetailViewModelBase, IMeetingDetailViewModel
    {
        private readonly IMeetingService _meetingService;
        private readonly IMessageDialogService _messageDialogService;
        private MeetingModelWrapper _meeting;

        public MeetingDetailViewModel(
            IMeetingService meetingService,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService): base(eventAggregator)
        {
            this._meetingService = meetingService;
            this._messageDialogService = messageDialogService;
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


        public override async Task LoadAddableAsync()
        {
            var newMeeting = await CreateMeeting();

            InitializeMeeting(newMeeting);
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
            RaiseDetailSaveEvent(Meeting.Id, Meeting.Title);
        }
    }
}
