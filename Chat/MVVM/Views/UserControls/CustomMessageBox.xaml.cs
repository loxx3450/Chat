using Chat.MVVM.Core;
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
        //Timer
        private Timer _timeoutTimer;


        //Caption
        public string Caption { get; set; }
        public SolidColorBrush CaptionBrush { get; set; }


        //Message
        public string Message { get; set; }


        //Commands
        public RelayCommand ClickPositiveCommand { get; set; }
        public RelayCommand ClickNegativeCommand { get; set; }
        

        public CustomMessageBox(MessageBoxType messageType, string message, int timeout = -1)
        {
            InitializeComponent();

            //Setting main properties of MessageBox
            Message = message;
            Caption = messageType.ToString();

            //Initializing Commands
            ClickPositiveCommand = new RelayCommand((o) =>
            {
                DialogResult = true;
                Close();
            });

            ClickNegativeCommand = new RelayCommand((o) =>
            {
                DialogResult = false;
                Close();
            });

            //Showing buttons depending on MessageBox's type
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

            //Setting timer (opt.)
            if (timeout >= 0)
                _timeoutTimer = new Timer(OnTimerElapsed, null, timeout, Timeout.Infinite);
        }

        private void OnTimerElapsed(object state)
        {
            Dispatcher.Invoke(() =>
            {
                Close();
            });
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
