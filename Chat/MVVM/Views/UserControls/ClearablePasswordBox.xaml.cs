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
        //Background
        public new static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(ClearablePasswordBox), 
                new PropertyMetadata(null, (d, e) => (d as ClearablePasswordBox).PassBoxBackground.Fill = (Brush)e.NewValue));

        public new Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }


        //Roundation
        public static readonly DependencyProperty RoundationProperty =
            DependencyProperty.Register(nameof(Roundation), typeof(double), typeof(ClearablePasswordBox),
                new PropertyMetadata(0.0, (d, e) =>
                {
                    ClearablePasswordBox filledRoundedBorder = d as ClearablePasswordBox;
                    double value = (double)e.NewValue;

                    filledRoundedBorder.PassBoxBorder.CornerRadius = new CornerRadius(value);
                    filledRoundedBorder.PassBoxBackground.RadiusX = value;
                    filledRoundedBorder.PassBoxBackground.RadiusY = value;
                }));

        public double Roundation
        {
            get => (double)GetValue(RoundationProperty);
            set => SetValue(RoundationProperty, value);
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
