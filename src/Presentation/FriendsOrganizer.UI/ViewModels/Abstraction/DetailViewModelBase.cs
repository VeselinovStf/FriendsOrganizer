﻿using FriendsOrganizer.UI.Events;
using FriendsOrganizer.UI.Events.Arguments;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendsOrganizer.UI.ViewModels.Abstraction
{
    public abstract class DetailViewModelBase : ViewModelBase, IDetailViewModel
    {
        private bool _hasChanges;
        protected readonly IEventAggregator _eventAggregator;

        public DetailViewModelBase(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;

            this.SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            this.DeleteCommand = new DelegateCommand(OnDeleteExecute);
        }

        public ICommand SaveCommand { get; }

        public ICommand DeleteCommand { get; }

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