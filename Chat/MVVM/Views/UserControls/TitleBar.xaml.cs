﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace Chat.MVVM.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для TitleBar.xaml
    /// </summary>
    public partial class TitleBar : UserControl
    {
        private Window Window
        {
            get => Window.GetWindow(this);
        }


        public TitleBar()
        {
            InitializeComponent();

            RefreshMaximizeRestoreButton();

            Loaded += OnSourceInitialized;
        }


        //callbacks to events
        private void buttonMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (Window.WindowState == WindowState.Maximized)
            {
                Window.WindowState = WindowState.Normal;
            }
            else
            {
                Window.WindowState = WindowState.Maximized;
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Window?.Close();
        }

        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            Window.WindowState = WindowState.Minimized;
        }


        private void RefreshMaximizeRestoreButton()
        {
            if (Window?.WindowState == WindowState.Maximized)
            {
                buttonMaximize.Visibility = Visibility.Collapsed;
                buttonRestore.Visibility = Visibility.Visible;
            }
            else
            {
                buttonMaximize.Visibility = Visibility.Visible;
                buttonRestore.Visibility = Visibility.Collapsed;
            }
        }

        private void StateChanged(object sender, EventArgs e)
        {
            RefreshMaximizeRestoreButton();
        }

        private void OnSourceInitialized(object sender, EventArgs e)
        {
            ((HwndSource)PresentationSource.FromVisual(this)).AddHook(HookProc);

            if (Window != null)
            {
                Window.StateChanged += StateChanged;
            }
        }


        //Service methods to render Control correctly, when Window will be maximized
        #region ServiceInfo
        public static IntPtr HookProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_GETMINMAXINFO)
            {
                // We need to tell the system what our size should be when maximized. Otherwise it will cover the whole screen,
                // including the task bar.
                MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

                // Adjust the maximized size and position to fit the work area of the correct monitor
                IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

                if (monitor != IntPtr.Zero)
                {
                    MONITORINFO monitorInfo = new MONITORINFO();
                    monitorInfo.cbSize = Marshal.SizeOf(typeof(MONITORINFO));
                    GetMonitorInfo(monitor, ref monitorInfo);
                    RECT rcWorkArea = monitorInfo.rcWork;
                    RECT rcMonitorArea = monitorInfo.rcMonitor;
                    mmi.ptMaxPosition.X = Math.Abs(rcWorkArea.Left - rcMonitorArea.Left);
                    mmi.ptMaxPosition.Y = Math.Abs(rcWorkArea.Top - rcMonitorArea.Top);
                    mmi.ptMaxSize.X = Math.Abs(rcWorkArea.Right - rcWorkArea.Left);
                    mmi.ptMaxSize.Y = Math.Abs(rcWorkArea.Bottom - rcWorkArea.Top);
                }

                Marshal.StructureToPtr(mmi, lParam, true);
            }

            return IntPtr.Zero;
        }

        private const int WM_GETMINMAXINFO = 0x0024;

        private const uint MONITOR_DEFAULTTONEAREST = 0x00000002;

        [DllImport("user32.dll")]
        private static extern IntPtr MonitorFromWindow(IntPtr handle, uint flags);

        [DllImport("user32.dll")]
        private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.Left = left;
                this.Top = top;
                this.Right = right;
                this.Bottom = bottom;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MONITORINFO
        {
            public int cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public uint dwFlags;
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }
        #endregion
    }
}
