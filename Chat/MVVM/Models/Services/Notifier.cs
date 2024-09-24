using Chat.MVVM.Views.UserControls;
using Chat.MVVM.Views.UserControls.AdditionalInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chat.MVVM.Models.Services
{
    public static class Notifier
    {
        public static bool Notify(MessageBoxType messageType, string message)
        {
            return Application.Current.Dispatcher.Invoke(() =>
            {
                CustomMessageBox messageBox = new CustomMessageBox(messageType, message);
                messageBox.Owner = Application.Current.MainWindow;

                return (bool)messageBox.ShowDialog();
            });
        }

        public static bool Notify(MessageBoxType messageType, string message, int timeout)
        {
            return Application.Current.Dispatcher.Invoke(() =>
            {
                CustomMessageBox messageBox = new CustomMessageBox(messageType, message, timeout);
                messageBox.Owner = Application.Current.MainWindow;

                return (bool)messageBox.ShowDialog();
            });
        }
    }
}
