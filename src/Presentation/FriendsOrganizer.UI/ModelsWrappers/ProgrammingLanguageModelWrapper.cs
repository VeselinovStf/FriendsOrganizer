using FriendsOrganizer.Data.Models;

namespace FriendsOrganizer.UI.ModelsWrappers
{

    public class ProgrammingLanguageModelWrapper : ModelWrapperBase<ProgrammingLanguage>
    {
        public ProgrammingLanguageModelWrapper(ProgrammingLanguage model) : base(model)
        {
        }

        public int Id
        {
            get { return GetValue<int>(); }

        }

        public string FriendId
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

        public string Name
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
