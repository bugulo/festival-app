using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Festival.App.Services.MessageDialog
{
    public partial class MessageDialog : Window
    {
        private MessageDialogResult _result;

        public MessageDialog(string title, string text, MessageDialogResult defaultResult,
            MessageDialogButtonConfiguration buttonConfiguration)
        {
        }

        public new MessageDialogResult ShowDialog()
        {
            base.ShowDialog();
            return _result;
        }
    }
}