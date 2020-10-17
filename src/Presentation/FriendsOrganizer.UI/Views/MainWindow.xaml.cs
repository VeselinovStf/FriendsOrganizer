using FriendsOrganizer.UI.ViewModels;
using System.Windows;

namespace FriendsOrganizer.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            this._mainWindowViewModel = mainWindowViewModel;
            DataContext = this._mainWindowViewModel;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await this._mainWindowViewModel.Load();
        }
    }
}
