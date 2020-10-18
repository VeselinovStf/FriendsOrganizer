using FriendsOrganizer.UI.Models;
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
                ValidateError();
            }
        }
    
        private void ValidateError([CallerMemberName]string propertyName = null)
        {
            ClearError(propertyName);
            AddError(propertyName, "aaaaaa");
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
    } 
}
