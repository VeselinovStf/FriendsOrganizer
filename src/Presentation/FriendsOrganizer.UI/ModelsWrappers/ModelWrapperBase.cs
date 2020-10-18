using FriendsOrganizer.UI.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            ValidatePropertyInternal(propertyName, value);
        }

        private void ValidatePropertyInternal(string propertyName, object currentValue)
        {
            ClearError(propertyName);

            ValidateDataAnnotation(propertyName, currentValue);

            ValidateCutomErrors(propertyName);
        }

        private void ValidateCutomErrors(string propertyName)
        {
            var errors = ValidateProperty(propertyName);
            foreach (var error in errors)
            {
                AddError(propertyName, error);
            }
        }

        private void ValidateDataAnnotation(string propertyName, object currentValue)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(Model) { MemberName = propertyName };

            Validator.TryValidateProperty(currentValue,validationContext, validationResults);

            foreach (var result in validationResults)
            {
                AddError(propertyName, result.ErrorMessage);
            }
        }

        protected virtual IEnumerable<string> ValidateProperty(string propertyName)
        {
            return null;
        }
    }

   
}
