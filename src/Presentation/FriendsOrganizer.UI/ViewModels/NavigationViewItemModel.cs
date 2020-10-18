namespace FriendsOrganizer.UI.ViewModels
{
    public class NavigationViewItemModel : ViewModelBase
    {
        public NavigationViewItemModel(int id, string displayProperty)
        {
            Id = id;
            DisplayProperty = displayProperty;
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _displayProperty;

        public string DisplayProperty
        {
            get { return _displayProperty; }
            set 
            { 
                _displayProperty = value;
                OnPropertyChanged();
            }
        }


    }
}
