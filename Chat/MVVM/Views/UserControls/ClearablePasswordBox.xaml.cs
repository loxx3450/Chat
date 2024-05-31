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


        //BorderThickness
        public new static DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register(nameof(BorderThickness), typeof(Thickness), typeof(ClearablePasswordBox),
                new PropertyMetadata(new Thickness(), (d, e) => (d as ClearablePasswordBox).PassBoxBorder.BorderThickness = (Thickness)e.NewValue));

        public new Thickness BorderThickness
        {
            get => (Thickness)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }


        //PlaceholderText
        public static DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(nameof(PlaceholderText), typeof(string), typeof(ClearablePasswordBox),
                new PropertyMetadata(string.Empty, (d, e) => (d as ClearablePasswordBox).Placehold.Text = (string)e.NewValue));

        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
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
