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
    /// <summary>
    /// Логика взаимодействия для ClearableTextBox.xaml
    /// </summary>
    public partial class ClearableTextBox : InputBox
    {
        //Text
        public static DependencyProperty TextProperty = 
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(ClearableTextBox),
                new PropertyMetadata(string.Empty));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }


        public ClearableTextBox()
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


        //Handlers
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBox.Text.Length > 0)
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

        private void ClearTextBtn_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Clear();
            TextBox.Focus();

            DeactivateButton();
        }

        private void Grid_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox.Text.Length > 0)
                ActivateButton();
        }

        private void Grid_LostFocus(object sender, RoutedEventArgs e)
        {
            DeactivateButton();
        }
    }
}
