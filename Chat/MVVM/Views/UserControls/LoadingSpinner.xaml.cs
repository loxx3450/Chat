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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chat.MVVM.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для LoadingSpinner.xaml
    /// </summary>
    public partial class LoadingSpinner : UserControl
    {
        //Diameter
        public static DependencyProperty DiameterProperty =
            DependencyProperty.Register(nameof(Diameter), typeof(double), typeof(LoadingSpinner), 
                new PropertyMetadata(50.0));

        public double Diameter
        {
            get => (double)GetValue(DiameterProperty);
            set => SetValue(DiameterProperty, value);
        }


        //Stroke Thickness
        public static DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register(nameof(StrokeThickness), typeof(double), typeof(LoadingSpinner), 
                new PropertyMetadata(1.0));

        public double StrokeThickness
        {
            get => (double)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }


        //Stroke
        public static DependencyProperty StrokeProperty =
            DependencyProperty.Register(nameof(Stroke), typeof(Brush), typeof(LoadingSpinner),
                new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public Brush Stroke
        {
            get => (Brush)GetValue(StrokeProperty);
            set => SetValue(StrokeProperty, value);
        }


        //Duration
        public static DependencyProperty DurationProperty =
            DependencyProperty.Register(nameof(Duration), typeof(int), typeof(LoadingSpinner),
                new PropertyMetadata(1000, (d,e) =>
                {
                    LoadingSpinner obj = (LoadingSpinner)d;

                    obj.DoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds((int)e.NewValue));

                    obj.RestartStoryboard();
                }));

        public int Duration
        {
            get => (int)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }


        //Storyboard's state
        public DoubleAnimation DoubleAnimation { get; set; }

        private Storyboard storyboard;


        public LoadingSpinner()
        {
            InitializeComponent();
            InitializeStoryboard();

            Stroke = new SolidColorBrush(Colors.White);
        }

        private void InitializeStoryboard()
        {
            DoubleAnimation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = new Duration(TimeSpan.FromMilliseconds(Duration)),
                EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseInOut }
            };

            storyboard = new Storyboard()
            {
                RepeatBehavior = RepeatBehavior.Forever
            };
            
            Storyboard.SetTarget(DoubleAnimation, Spinner);
            Storyboard.SetTargetProperty(DoubleAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));

            storyboard.Children.Add(DoubleAnimation);

            storyboard.Begin();
        }

        private void RestartStoryboard()
        {
            storyboard.Stop();
            storyboard.Begin();
        }
    }
}
