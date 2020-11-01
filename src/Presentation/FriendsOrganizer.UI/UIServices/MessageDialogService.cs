using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using System.Windows;

namespace FriendsOrganizer.UI.UIServices
{
    public class MessageDialogService : IMessageDialogService
    {
        private MetroWindow MetroWindow => (MetroWindow)App.Current.MainWindow;

        public async Task ShowInfoDialogAsync(string text)
        {
            await MetroWindow.ShowMessageAsync("Info", text);
        }

        public async Task<MessageDialogResult> ShowOkCancelDialogAsync(string text, string title)
        {
            var metroWindow = this.MetroWindow;
            var result = await metroWindow.ShowMessageAsync(title, text, MessageDialogStyle.AffirmativeAndNegative);

            return result == MahApps.Metro.Controls.Dialogs.MessageDialogResult.Affirmative?
                 MessageDialogResult.Ok :
                 MessageDialogResult.Cancel;
        }

       
    }
    public enum MessageDialogResult
    {
        Ok,
        Cancel
    }
}
