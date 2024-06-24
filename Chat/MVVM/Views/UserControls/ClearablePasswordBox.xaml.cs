using Chat.MVVM.Views.UserControls.Additional_Infrastructure;
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

namespace Chat.MVVM.Views.UserControls
{
    public partial class ClearablePasswordBox : InputBox
    {
        //Password
        public static DependencyProperty PasswordProperty =
            DependencyProperty.Register(nameof(Password), typeof(string), typeof(ClearablePasswordBox),
                new PropertyMetadata(string.Empty));

        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        public ClearablePasswordBox()
        {
            InitializeComponent();
        }


        //private Methods
        private void ActivateButton()
        {
            ClearTextBtn.Visibility = Visibility.Visible;
            ColumnWithButton.Width = new GridLength(36);
        }

        private void DeactivateButton()
        {
            ClearTextBtn.Visibility = Visibility.Collapsed;
            ColumnWithButton.Width = new GridLength();
        }


        private void ClearTextBtn_Click(object sender, RoutedEventArgs e)
        {
            PassBox.Clear();
            PassBox.Focus();

            DeactivateButton();
        }

        private void PassBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PassBox.Password.Length > 0)
            {
                ActivateButton();
                Placehold.Visibility = Visibility.Collapsed;
            }
            else
            {
                DeactivateButton();
                Placehold.Visibility = Visibility.Visible;
            }
        }

        private void PassBoxGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PassBox.Password.Length > 0)
                ActivateButton();
        }

        private void PassBoxGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            DeactivateButton();
        }
    }
}
