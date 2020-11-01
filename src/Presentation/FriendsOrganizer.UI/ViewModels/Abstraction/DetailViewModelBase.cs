using FriendsOrganizer.UI.Events;
using FriendsOrganizer.UI.Events.Arguments;
using FriendsOrganizer.UI.UIServices;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Events;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendsOrganizer.UI.ViewModels.Abstraction
{
    public abstract class DetailViewModelBase : ViewModelBase, IDetailViewModel
    {
        private bool _hasChanges;
        private int _id;
        private string _title;
        protected readonly IEventAggregator _eventAggregator;
        protected readonly IMessageDialogService _messageDialogService;

        public DetailViewModelBase(
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService
            )
        {
            this._eventAggregator = eventAggregator;
            this._messageDialogService = messageDialogService;
            this.SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            this.DeleteCommand = new DelegateCommand(OnDeleteExecute);
            this.CloseCommand = new DelegateCommand(OnCloseExecute);
        }

        protected virtual async void OnCloseExecute()
        {
            if (HasChange)
            {
                var result = await this._messageDialogService
                    .ShowOkCancelDialogAsync("You've made changes. Close this item?", "Question");

                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            _eventAggregator.GetEvent<AfterDetailsCloseEvent>()
                .Publish(new AfterDetailsCloseEventArgs()
                {
                    Id = this.Id,
                    ViewModelName = this.GetType().Name
                });
        }

        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }

        public ICommand DeleteCommand { get; }

       

        public string Title
        {
            get { return _title; }
            set 
            { 
                _title = value;
                OnPropertyChanged();
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

        public int Id
        {
            get
            {
                return _id;
            }
            protected set
            {
                _id = value;
            }
        }

        protected async Task OnSaveExecuteWithOptimisticConcurrency(
            Func<Task> saveFunc,
            Action afterSaveAction)
        {
            try
            {
                await saveFunc();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var dbValue = ex.Entries.Single().GetDatabaseValues();

                if (dbValue == null)
                {
                    await this._messageDialogService.ShowInfoDialogAsync("Thi item is deleted by another user");
                    RaiseDetailDeleteEvent(Id);
                    return;
                }


                var result = await this._messageDialogService
                    .ShowOkCancelDialogAsync("The entity is been changed. Click Ok to save your changes anyway", "Edited by other user");

                if (result == MessageDialogResult.Ok)
                {
                    //Client wins
                    var entry = ex.Entries.Single();
                    entry.OriginalValues.SetValues(await entry.GetDatabaseValuesAsync());

                    await saveFunc();
                }
                else
                {
                    //Db wins
                    await ex.Entries.Single().ReloadAsync();
                    await LoadAsync(Id);
                }

            }

            afterSaveAction();

        }
        protected abstract void OnDeleteExecute();

        protected abstract bool OnSaveCanExecute();

        protected abstract void OnSaveExecute();

        public abstract Task LoadAddableAsync();

        public abstract Task LoadAsync(int id);

        protected virtual void RaiseDetailDeleteEvent(int modelId)
        {
            this._eventAggregator.GetEvent<AfterDeleteEvent>().Publish(
                new AfterDeleteEventArgs()
                {
                    Id = modelId,
                    ViewModelName = this.GetType().Name
                });
        }

        protected virtual void RaiseCollectionSaveEvent()
        {
            this._eventAggregator.GetEvent<AfterCollectionSaveEvent>().Publish(
                new AfterCollectionSaveEventArgs()
                {
                    ViewModelName = this.GetType().Name
                });
        }

        protected virtual void RaiseDetailSaveEvent(int modelId, string displayProperty)
        {
            this._eventAggregator.GetEvent<AfterSaveDetailsEvent>().Publish(
                new AfterSaveDetailsEventArgs()
                {
                    Id = modelId,
                    DisplayProperty = displayProperty,
                    ViewModelName = this.GetType().Name
                });
        }

    }
}
