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
    public partial class ClearableTextBox : UserControl
    {
        //Roundation
        public static DependencyProperty RoundationProperty =
            DependencyProperty.Register(nameof(Roundation), typeof(double), typeof(ClearableTextBox),
                new PropertyMetadata(0.0, (d, e) =>
                {
                    ClearableTextBox obj = d as ClearableTextBox;
                    double value = (double)e.NewValue;

                    obj.Border.CornerRadius = new CornerRadius(value);
                    obj.Rect.RadiusX = value;
                    obj.Rect.RadiusY = value;
                }));

        public double Roundation
        {
            get => (double)GetValue(RoundationProperty);
            set => SetValue(RoundationProperty, value);
        }


        //Background
        public static DependencyProperty BackgroundProperty =
            DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(ClearableTextBox),
                new PropertyMetadata(null, (d, e) => (d as ClearableTextBox).Rect.Fill = (Brush)e.NewValue));

        public new Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }


        //BorderThickness
        public new static DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register(nameof(BorderThickness), typeof(Thickness), typeof(ClearableTextBox),
                new PropertyMetadata(new Thickness(), (d, e) => (d as ClearableTextBox).Border.BorderThickness = (Thickness)e.NewValue));

        public new Thickness BorderThickness
        {
            get => (Thickness)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }


        //PlaceholderText
        public static DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(nameof(PlaceholderText), typeof(string), typeof(ClearableTextBox),
                new PropertyMetadata(string.Empty, (d, e) => (d as ClearableTextBox).Placehold.Text = (string)e.NewValue));

        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }


        //PlaceholderForeground
        public static DependencyProperty PlaceholderForegroundProperty =
            DependencyProperty.Register(nameof(PlaceholderForeground), typeof(Brush), typeof(ClearableTextBox),
                new PropertyMetadata(null, (d, e) => (d as ClearableTextBox).Placehold.Foreground = (Brush)e.NewValue));

        public Brush PlaceholderForeground
        {
            get => (Brush)GetValue(PlaceholderForegroundProperty);
            set => SetValue(PlaceholderForegroundProperty, value);
        }


        //PlaceholderFontSize
        public static DependencyProperty PlaceholderFontSizeProperty = 
            DependencyProperty.Register(nameof(PlaceholderFontSize), typeof(double), typeof(ClearableTextBox),
                new PropertyMetadata(0.0, (d, e) => (d as ClearableTextBox).Placehold.FontSize = (double)e.NewValue));

        public double PlaceholderFontSize
        {
            get => (double)GetValue(PlaceholderFontSizeProperty);
            set => SetValue(PlaceholderFontSizeProperty, value);
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
