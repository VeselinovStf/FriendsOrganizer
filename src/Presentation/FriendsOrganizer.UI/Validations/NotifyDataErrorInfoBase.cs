using FriendsOrganizer.UI.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FriendsOrganizer.UI.Validations
{
    public class NotifyDataErrorInfoBase : ViewModelBase, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> _errorMessages =
          new Dictionary<string, List<string>>();

        public bool HasErrors => this._errorMessages.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;


        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected void AddError(string propertyName, string error)
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

        protected void ClearError(string propertyName)
        {
            if (_errorMessages.ContainsKey(propertyName))
            {
                _errorMessages.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        {
            return this._errorMessages.ContainsKey(propertyName) ?
              this._errorMessages[propertyName] : null;
        }
    }
}
