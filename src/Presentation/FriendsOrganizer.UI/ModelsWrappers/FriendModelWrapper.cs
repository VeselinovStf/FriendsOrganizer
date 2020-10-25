using FriendsOrganizer.Data.Models;
using System.Collections;
using System.Collections.Generic;

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

        public int? ProgrammingLanguageId
        {
            get 
            { 
                return GetValue<int?>(); 
            }
            set
            {
                SetValue(value);
            }

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

        public ICollection<FriendPhoneNumber> PhoneNumbers
        {
            get
            {
                return GetValue<ICollection<FriendPhoneNumber>>();
            }
            set
            {
                SetValue(value);
            }
        }
    } 
}
