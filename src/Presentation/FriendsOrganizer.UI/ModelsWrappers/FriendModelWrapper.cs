using FriendsOrganizer.Data.Models;

namespace FriendsOrganizer.UI.ModelsWrappers
{
    public class FriendModelWrapper : ModelWrapperBase<Friend>
    {
        public FriendModelWrapper(Friend model) : base(model)
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
    } 
}
