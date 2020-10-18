using FriendsOrganizer.UI.Models;
using FriendsOrganizer.UI.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FriendsOrganizer.UI.ModelsWrappers
{
    public class FriendModelWrapper : ViewModelBase, INotifyDataErrorInfo
    {
        public FriendModelWrapper(FriendModel model)
        {
            Model = model;
        }

        public FriendModel Model { get; set; }

        public int Id
        {
            get { return Model.Id; }
           
        }

        public string FirstName
        {
            get
            {
                return this.Model.FirstName;
            }
            set
            {
                this.Model.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get
            {
                return this.Model.LastName;
            }
            set
            {
                this.Model.LastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get
            {
                return this.Model.Email;
            }
            set
            {
                this.Model.Email = value;
                OnPropertyChanged();
            }
        }

        private Dictionary<string, List<string>> _errorMessages =
            new Dictionary<string, List<string>>();

        public bool HasErrors => this._errorMessages.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return this._errorMessages.ContainsKey(propertyName) ?
                this._errorMessages[propertyName] : null;
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void AddError(string propertyName, string error)
        {
            if (!_errorMessages.ContainsKey(propertyName))
            {
                _errorMessages[propertyName] = new List<string>();
            }

            if (!_errorMessages[propertyName].Contains(error))
            {
                _errorMessages[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        private void ClearError(string propertyName)
        {
            if (_errorMessages.ContainsKey(propertyName))
            {
                _errorMessages.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
    }
}
