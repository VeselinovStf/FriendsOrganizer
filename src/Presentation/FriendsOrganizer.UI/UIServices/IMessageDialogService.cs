﻿

namespace FriendsOrganizer.UI.UIServices
{
    public interface IMessageDialogService
    {
        MessageDialogResult ShowOkCancelDialog(string text, string title);
    }
}