using FriendsOrganizer.UI.Validations;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FriendsOrganizer.UI.ModelsWrappers
{
    public class ModelWrapperBase<T> : NotifyDataErrorInfoBase
    {
        public T Model { get; set; }
        public ModelWrapperBase(T model)
        {
            this.Model = model;
        }

        protected virtual TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            return (TValue)(typeof (T).GetProperty(propertyName).GetValue(Model));
        }

        protected virtual void SetValue<TValue>(TValue value,[CallerMemberName]string propertyName = null)
        {
            typeof(T).GetProperty(propertyName).SetValue(Model, value);
            OnPropertyChanged();
            ValidatePropertyInternal(propertyName);
        }

        private void ValidatePropertyInternal(string propertyName)
        {
            ClearError(propertyName);

            var errors = ValidateProperty(propertyName);

            foreach (var error in errors)
            {
                AddError(propertyName, error);
            }
        }

        protected virtual IEnumerable<string> ValidateProperty(string propertyName)
        {
            return null;
        }
    }

   
}
