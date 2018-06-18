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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FastType
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void BExit_Click(object sender, RoutedEventArgs e)
        {
            Opacity = 1;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(DispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if ((Opacity -= 0.1) <= 0)
            {
                Close();
            }
        }

        private void BBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.tbLogin.Text = Common.Login;
            main.Show();
            Close();
        }

        private void BSingle_Click(object sender, RoutedEventArgs e)
        {
            Single single = new Single();
            single.LoginBox.Text = Common.Login;
            single.Show();
            Close();
        }

        private void bMulty_Click(object sender, RoutedEventArgs e)
        {
            Connection connection = new Connection();
            connection.Show();
            Close();
        }

        private void bRecords_Click(object sender, RoutedEventArgs e)
        {
            Records records = new Records();
            records.Show();
            Close();
        }
    }
}
