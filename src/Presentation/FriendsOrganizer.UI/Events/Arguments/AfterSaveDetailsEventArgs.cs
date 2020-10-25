namespace FriendsOrganizer.UI.Events.Arguments
{
    public class AfterSaveDetailsEventArgs
    {
        public int Id { get; set; }

        public string DisplayProperty { get; set; }

        public string ViewModelName { get; set; }
    }
}
