using FriendsOrganizer.UI.Models;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FriendsOrganizer.UI.ModelsWrappers
{
    public class FriendModelWrapper : ModelWrapperBase<FriendModel>
    {
        public FriendModelWrapper(FriendModel model) : base(model)
        {
        }

        public int Id
        {
            get { return GetValue<int>(); }
           
        }

        public string FirstName
        {
            get
            {
                return GetValue<string>();
            }
            set
            {
                SetValue(value);
            }
        }
    

        public string LastName
        {
            get
            {
                return GetValue<string>();
            }
            set
            {
                SetValue(value);
            }
        }

        public string Email
        {
            get
            {
                return GetValue<string>();
            }
            set
            {
                SetValue(value);
            }
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(FirstName):
                    if (string.IsNullOrWhiteSpace(FirstName) ||
                        FirstName.Length < 4 || FirstName.Length > 20)
                    {
                        yield return "Ivalid First Name";
                    }
                    break;
                default:
                    break;
            }
        }
    } 
}
