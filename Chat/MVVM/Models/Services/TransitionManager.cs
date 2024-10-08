﻿using Chat.MVVM.Views.UserControls;
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
        //Windows
        private static LoadingWindow loadingWindow = null!;
        private static Window background = null!;
        private static Window currentWindow = null!;


        //Counter for controlling Windows's states
        private static int stateCounter = 0;


        //Lock
        private static object lockObj = new object();


        public static void PutWaitingIn(int milliseconds)
        {
            Task.Delay(milliseconds).ContinueWith(o => PutWaiting());
        }

        public static void PutWaiting()
        {
            lock (lockObj)
            {
                if (stateCounter == 0)
                {
                    //Calling method in Dispatcher's Thread
                    Application.Current.Dispatcher.Invoke(SetupAndOpenWindows);
                }

                stateCounter++;
            }
        }

        public static void RemoveWaiting()
        {
            lock (lockObj)
            {
                stateCounter--;

                if (stateCounter == 0)
                {
                    //Calling method in Dispatcher's Thread
                    Application.Current.Dispatcher.Invoke(CloseWindows);
                }
            }
        }

        private static void CloseWindows()
        {
            loadingWindow.Owner = null;
            background.Owner = null;

            loadingWindow.Close();
            background.Close();
        }

        private static void SetupAndOpenWindows()
        {
            //Current Window
            currentWindow = Application.Current.MainWindow;

            currentWindow.StateChanged += CurrentWindow_StateChanged;


            //Background Window
            background = new Window()
            {
                AllowsTransparency = true,
                Background = Brushes.Black,
                Opacity = 0.4,
                WindowStyle = WindowStyle.None,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = currentWindow,
                Width = currentWindow.ActualWidth,
                Height = currentWindow.ActualHeight,
                ShowInTaskbar = false
            };

            background.Show();


            //Loading Window
            loadingWindow = new LoadingWindow(background);

            loadingWindow.Show();
        }

        private static void CurrentWindow_StateChanged(object? sender, EventArgs e)
        {
            background.WindowState = currentWindow.WindowState;
            loadingWindow.WindowState = currentWindow.WindowState;
        }
    }
}
