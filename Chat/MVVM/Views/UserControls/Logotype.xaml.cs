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
    /// Логика взаимодействия для Logotype.xaml
    /// </summary>
    public partial class Logotype : UserControl
    {
        public new DependencyProperty HeightProperty =
            DependencyProperty.Register(nameof(Height), typeof(double), typeof(Logotype),
                new PropertyMetadata(0.0, (d, e) => (d as Logotype).Logo.Height = (double)e.NewValue));

        public new double Height
        {
            get => (double)GetValue(HeightProperty);
            set => SetValue(HeightProperty, value);
        }


        public Logotype()
        {
            InitializeComponent();
        }
    }
}
