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
    public partial class ClearablePasswordBox : UserControl
    {
        public new static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register(nameof(BorderBrush), typeof(Brush), typeof(ClearablePasswordBox), 
                new PropertyMetadata(null, (d, e) => (d as ClearablePasswordBox).PassBoxBorder.BorderBrush = (Brush)e.NewValue));

        public new Brush BorderBrush
        {
            get => (Brush)GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }


        public new static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register(nameof(FontSize), typeof(double), typeof(ClearablePasswordBox), 
                new PropertyMetadata(2.0, (d, e) => (d as ClearablePasswordBox).PassBox.FontSize = (double)e.NewValue));

        public new double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }


        public new static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(ClearablePasswordBox), 
                new PropertyMetadata(null, (d, e) => (d as ClearablePasswordBox).PassBoxBackground.Fill = (Brush)e.NewValue));

        public new Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }


        public ClearablePasswordBox()
        {
            InitializeComponent();
        }


        private void ClearTextBtn_Click(object sender, RoutedEventArgs e)
        {
            PassBox.Clear();
            PassBox.Focus();

            ClearTextBtn.Visibility = Visibility.Collapsed;
        }

        private void PassBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PassBox.Password.Length > 0)
                ClearTextBtn.Visibility = Visibility.Visible;
            else
                ClearTextBtn.Visibility = Visibility.Collapsed;
        }

        private void PassBoxGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PassBox.Password.Length > 0)
                ClearTextBtn.Visibility = Visibility.Visible;
        }

        private void PassBoxGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            ClearTextBtn.Visibility = Visibility.Collapsed;
        }
    }
}
