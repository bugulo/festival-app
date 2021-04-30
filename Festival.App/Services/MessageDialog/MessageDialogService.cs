using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival.App.Services.MessageDialog
{
    public class MessageDialogService : IMessageDialogService
    {
        public MessageDialogResult Show(
            string title,
            string caption,
            MessageDialogButtonConfiguration buttonConfiguration,
            MessageDialogResult defaultResult)
        {
            var messageDialog = new MessageDialog(title, caption, defaultResult, buttonConfiguration)
            {
                Owner = Application.Current.MainWindow
            };
            return messageDialog.ShowDialog();
        }
    }
}
