using Chat.MVVM.Views.UserControls.AdditionalInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Chat.MVVM.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        private Timer _timeoutTimer;

        public string Caption { get; set; }
        public SolidColorBrush CaptionBrush { get; set; }

        public string Message { get; set; }

        public CustomMessageBox(MessageBoxType messageType, string message, int timeout = -1)
        {
            InitializeComponent();

            Message = message;
            Caption = messageType.ToString();

            switch (messageType) 
            {
                case MessageBoxType.Info:
                    btnOk.Visibility = Visibility.Visible;
                    CaptionBrush = new SolidColorBrush(Colors.White);
                    break;

                case MessageBoxType.Success:
                    btnOk.Visibility = Visibility.Visible;
                    CaptionBrush = new SolidColorBrush(Colors.Green);
                    break;

                case MessageBoxType.Warning:
                    btnOk.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Visible;
                    CaptionBrush = new SolidColorBrush(Colors.Orange);
                    break;

                case MessageBoxType.Error:
                    btnOk.Visibility = Visibility.Visible;
                    CaptionBrush = new SolidColorBrush(Colors.Red);
                    break;

                case MessageBoxType.Confirmation:
                    btnYes.Visibility = Visibility.Visible;
                    btnNo.Visibility = Visibility.Visible;
                    CaptionBrush = new SolidColorBrush(Colors.Blue);
                    break;
            }

            if (timeout >= 0)
                _timeoutTimer = new Timer(OnTimerElapsed, null, timeout, Timeout.Infinite);
        }

        private void OnTimerElapsed(object state)
        {
            Dispatcher.Invoke(() =>
            {
                DialogResult = false;
                Close();
            });
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void clickPositive(object sender, RoutedEventArgs e) 
        {
            DialogResult = true;
            Close();
        }

        private void clickNegative(object sender, RoutedEventArgs e) 
        { 
            DialogResult = false;
            Close();
        }
    }
}
