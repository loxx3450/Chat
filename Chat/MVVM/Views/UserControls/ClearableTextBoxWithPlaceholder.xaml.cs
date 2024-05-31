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
    /// Логика взаимодействия для ClearableTextBoxWithPlaceholder.xaml
    /// </summary>
    public partial class ClearableTextBoxWithPlaceholder : UserControl
    {
        //Roundation
        public static DependencyProperty RoundationProperty =
            DependencyProperty.Register(nameof(Roundation), typeof(double), typeof(ClearableTextBoxWithPlaceholder),
                new PropertyMetadata(0.0, (d, e) =>
                {
                    ClearableTextBoxWithPlaceholder obj = d as ClearableTextBoxWithPlaceholder;
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
            DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(ClearableTextBoxWithPlaceholder),
                new PropertyMetadata(null, (d, e) => (d as ClearableTextBoxWithPlaceholder).Rect.Fill = (Brush)e.NewValue));

        public new Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }


        //BorderThickness
        public new static DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register(nameof(BorderThickness), typeof(Thickness), typeof(ClearableTextBoxWithPlaceholder),
                new PropertyMetadata(new Thickness(), (d, e) => (d as ClearableTextBoxWithPlaceholder).Border.BorderThickness = (Thickness)e.NewValue));

        public new Thickness BorderThickness
        {
            get => (Thickness)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }


        //PlaceholderText
        public static DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(nameof(PlaceholderText), typeof(string), typeof(ClearableTextBoxWithPlaceholder),
                new PropertyMetadata(string.Empty, (d, e) => (d as ClearableTextBoxWithPlaceholder).Placehold.Text = (string)e.NewValue));

        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }

        private string _placeholderText;

        public ClearableTextBoxWithPlaceholder()
        {
            InitializeComponent();

            _placeholderText = TextBox.Text;
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
