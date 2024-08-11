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
    /// Логика взаимодействия для LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        private const double OWNER_WIDTH_TO_WIDTH_RATIO = 5.0;
        private const double WIDTH_TO_HEIGHT_RATIO = 2.0;

        public LoadingWindow(Window owner)
        {
            InitializeComponent();

            Owner = owner;
            Width = owner.Width / OWNER_WIDTH_TO_WIDTH_RATIO;
            Height = Width / WIDTH_TO_HEIGHT_RATIO;
        }
    }
}
