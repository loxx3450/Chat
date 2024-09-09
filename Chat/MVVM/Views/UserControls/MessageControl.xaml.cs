using SharpVectors.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    /// Логика взаимодействия для MessageBox.xaml
    /// </summary>
    public partial class MessageControl : UserControl
    {
        //Background
        public static new readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(MessageControl),
                new PropertyMetadata(new SolidColorBrush(Colors.Wheat)));

        public new Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }


        //Text
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(MessageControl),
                new PropertyMetadata(string.Empty));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }


        //Time 
        public static readonly DependencyProperty TimeProperty = 
            DependencyProperty.Register(nameof(Time), typeof(string), typeof(MessageControl),
                new PropertyMetadata(string.Empty));

        public string Time
        {
            get => (string)GetValue(TimeProperty);
            set => SetValue(TimeProperty, value);
        }

        public MessageControl()
        {
            InitializeComponent();
        }

        //TEMP
        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            polygon.Points.Clear();

            if (e.HeightChanged)
            {
                polygon.Points.Add(new Point(7, ActualHeight - 11));
                polygon.Points.Add(new Point(20, ActualHeight));
                polygon.Points.Add(new Point(0, ActualHeight));
            }
        }
    }
}
