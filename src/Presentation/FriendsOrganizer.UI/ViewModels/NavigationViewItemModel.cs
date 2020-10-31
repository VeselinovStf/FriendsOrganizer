using FriendsOrganizer.UI.Events;
using FriendsOrganizer.UI.Events.Arguments;
using Prism.Commands;
using Prism.Events;
using System;
using System.Windows.Input;

namespace FriendsOrganizer.UI.ViewModels
{
    public class NavigationViewItemModel : ViewModelBase
    {
        private string _displayProperty;
        private readonly IEventAggregator _eventAggregator;
        public string _viewModelName { get; set; }
        private int _id;

        public NavigationViewItemModel(int id, string displayProperty,
            IEventAggregator eventAggregator,
            string viewModelName)
        {
            Id = id;
            DisplayProperty = displayProperty;
            this._eventAggregator = eventAggregator;
            this._viewModelName = viewModelName;
            this.OpenDetailsCommand = new DelegateCommand(OnOpenDetailsEvent);
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
      
        public string DisplayProperty
        {
            get { return _displayProperty; }
            set
            {
                _displayProperty = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenDetailsCommand { get; }

        private void OnOpenDetailsEvent()
        {
            this._eventAggregator.GetEvent<OpenDetailsEvent>()
                         .Publish(
                new OpenDetailEventArgs() 
                { 
                    Id = this.Id , 
                    ViewModelName = _viewModelName
                });
        }

    }
}
