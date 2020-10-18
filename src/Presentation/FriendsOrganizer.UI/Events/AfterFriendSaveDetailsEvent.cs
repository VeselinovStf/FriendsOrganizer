using Prism.Events;

namespace FriendsOrganizer.UI.Events
{
    public class AfterFriendSaveDetailsEvent : PubSubEvent<AfterFriendSaveDetailsLookup>
    {
    }

    public class AfterFriendSaveDetailsLookup
    {
        public int Id { get; set; }

        public string DisplayProperty { get; set; }
    }
}
