using FriendsOrganizer.Data.Models;

namespace FriendsOrganizer.UI.ModelsWrappers
{
    public class FriendPhoneModelWrapper : ModelWrapperBase<FriendPhoneNumber>
    {
        public FriendPhoneModelWrapper(FriendPhoneNumber model) : base(model)
        {
        }

        public string PhoneNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

    }
}
