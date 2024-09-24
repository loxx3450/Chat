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
    /// Логика взаимодействия для FilledRoundedBorder.xaml
    /// </summary>
    public partial class FilledRoundedBorder : UserControl
    {
        //BorderThickness
        public new static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register(nameof(BorderThickness), typeof(Thickness), typeof(FilledRoundedBorder),
                new PropertyMetadata(new Thickness()));

        public new Thickness BorderThickness
        {
            get => (Thickness)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }


        //Roundation
        public static readonly DependencyProperty RoundationProperty =
            DependencyProperty.Register(nameof(Roundation), typeof(double), typeof(FilledRoundedBorder),
                new PropertyMetadata(0.0));

        public double Roundation
        {
            get => (double)GetValue(RoundationProperty);
            set => SetValue(RoundationProperty, value);
        }


        //Background
        public new static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(FilledRoundedBorder),
                new PropertyMetadata(new SolidColorBrush(Colors.Wheat)));

        public new Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }



        public FilledRoundedBorder()
        {
            InitializeComponent();
        }
    }
}
