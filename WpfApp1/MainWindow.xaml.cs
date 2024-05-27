using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Class1 class1 = new Class1();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = class1;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            class1.Visibility1 = Visibility.Visible;
        }

        private void CheckBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            class1.Visibility1 = Visibility.Hidden;
        }
    }
}