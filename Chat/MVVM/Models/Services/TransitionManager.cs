using Chat.MVVM.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Chat.MVVM.Models.Services
{
    internal class TransitionManager
    {
        private static LoadingWindow loadingWindow = null!;

        private static Window background = null!;

        private static Window currentWindow = null!;


        public static void PutWaiting()
        {
            //Current Window's setup
            currentWindow = Application.Current.MainWindow;

            currentWindow.StateChanged += CurrentWindow_StateChanged;

            //Background Window's setup
            background = new Window()
            {
                AllowsTransparency = true,
                Background = Brushes.Black,
                Opacity = 0.4,
                WindowStyle = WindowStyle.None,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = currentWindow,
                Width = currentWindow.Width,
                Height = currentWindow.Height,
                ShowInTaskbar = false
            };

            background.Show();

            //Loading Window's setup
            loadingWindow = new LoadingWindow()
            {
                Owner = background
            };

            loadingWindow.Show();
        }

        private static void CurrentWindow_StateChanged(object? sender, EventArgs e)
        {
            background.WindowState = currentWindow.WindowState;
            loadingWindow.WindowState = currentWindow.WindowState;
        }

        public static void RemoveWaiting()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                loadingWindow.Owner = null;
                background.Owner = null;

                loadingWindow.Close();
                background.Close();
            });
        }
    }
}
