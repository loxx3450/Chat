using Chat.Core;
using Chat.MVVM;
using Chat.MVVM.Models.Services;
using Chat.MVVM.ViewModels;
using SocketEventLibrary.Sockets;
using System;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Chat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            SocketEventHandler.ConnectToServer();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //Setting MainWindow as StartWindow
            ServiceProvider.GetRequiredService<MainWindow>().Show();

            base.OnStartup(e);
        }
    }

}
