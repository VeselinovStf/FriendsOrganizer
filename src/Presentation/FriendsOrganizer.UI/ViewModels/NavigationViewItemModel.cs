using FriendsOrganizer.UI.Events;
using Prism.Commands;
using Prism.Events;
using System;
using System.Windows.Input;

namespace FriendsOrganizer.UI.ViewModels
{
    public class NavigationViewItemModel : ViewModelBase
    {
        public NavigationViewItemModel(int id, string displayProperty,
            IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayProperty = displayProperty;
            this._eventAggregator = eventAggregator;
            this.OpenFriendDetailsCommand = new DelegateCommand(OnOpenFriendDetailsEvent);
        }

   

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _displayProperty;
        private readonly IEventAggregator _eventAggregator;

        public string DisplayProperty
        {
            get { return _displayProperty; }
            set
            {
                _displayProperty = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenFriendDetailsCommand { get; }

        private void OnOpenFriendDetailsEvent()
        {
            this._eventAggregator.GetEvent<OpenFriendDetailsEvent>()
                         .Publish(this.Id);
        }

    }
}
