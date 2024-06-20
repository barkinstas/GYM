using GYM.Windows;
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

namespace GYM
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            Admin_Window x = new Admin_Window();
            x.Show();
            Close();
        }

        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            Manager_Window x = new Manager_Window();
            x.Show();
            Close();
        }

        private void Coach_Click(object sender, RoutedEventArgs e)
        {
            Coach_Window x = new Coach_Window();
            x.Show();
            Close();
        }
    }
}