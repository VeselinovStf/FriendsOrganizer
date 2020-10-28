using System.Windows;

namespace FriendsOrganizer.UI.UIServices
{
    public class MessageDialogService : IMessageDialogService
    {
        public void ShowInfoDialog(string text)
        {
            MessageBox.Show(text, "Info");
        }

        public MessageDialogResult ShowOkCancelDialog(string text, string title)
        {
            var result = MessageBox.Show(text, title, MessageBoxButton.OKCancel);

            return result == MessageBoxResult.OK ?
                MessageDialogResult.Ok : MessageDialogResult.Cancel;
        }

       
    }
    public enum MessageDialogResult
    {
        Ok,
        Cancel
    }
}
