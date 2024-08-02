using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Chat.MVVM.Views.UserControls.AdditionalInfrastructure
{
    public class InputBox : UserControl
    {
        //Background
        public new static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(InputBox),
                new PropertyMetadata(null));

        public new Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }


        //Roundation
        public static DependencyProperty RoundationProperty =
            DependencyProperty.Register(nameof(Roundation), typeof(double), typeof(InputBox),
                new PropertyMetadata(0.0));

        public double Roundation
        {
            get => (double)GetValue(RoundationProperty);
            set => SetValue(RoundationProperty, value);
        }


        //BorderThickness
        public new static DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register(nameof(BorderThickness), typeof(Thickness), typeof(InputBox),
                new PropertyMetadata(new Thickness()));

        public new Thickness BorderThickness
        {
            get => (Thickness)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }


        #region Placeholder
        //PlaceholderText
        public static DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(nameof(PlaceholderText), typeof(string), typeof(InputBox),
                new PropertyMetadata(string.Empty));

        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }

        //PlaceholderForeground
        public static DependencyProperty PlaceholderForegroundProperty =
            DependencyProperty.Register(nameof(PlaceholderForeground), typeof(Brush), typeof(InputBox),
                new PropertyMetadata(null));

        public Brush PlaceholderForeground
        {
            get => (Brush)GetValue(PlaceholderForegroundProperty);
            set => SetValue(PlaceholderForegroundProperty, value);
        }


        //PlaceholderFontSize
        public static DependencyProperty PlaceholderFontSizeProperty =
            DependencyProperty.Register(nameof(PlaceholderFontSize), typeof(double), typeof(InputBox),
                new PropertyMetadata(0.0));

        public double PlaceholderFontSize
        {
            get => (double)GetValue(PlaceholderFontSizeProperty);
            set => SetValue(PlaceholderFontSizeProperty, value);
        }
        #endregion


        //ErrorMessageFontSize
        public static DependencyProperty ErrorMessageFontSizeProperty =
            DependencyProperty.Register(nameof(ErrorMessageFontSize), typeof(double), typeof(InputBox),
                new PropertyMetadata(0.0));

        public double ErrorMessageFontSize
        {
            get => (double)GetValue(ErrorMessageFontSizeProperty);
            set => SetValue(ErrorMessageFontSizeProperty, value);
        }
    }
}
