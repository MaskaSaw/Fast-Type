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
using System.IO;
using System.Windows.Threading;

namespace FastType
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StreamWriter sw = new StreamWriter ("Auth.txt", true);
            sw.Close();
        }

        private void BAuth_Click(object sender, RoutedEventArgs e)
        {
            bool Exist = false;
            StreamReader sr = new StreamReader("Auth.txt");
            string Line = sr.ReadLine();
            while (Line != null)
            {
                if (Line.Contains(tbLogin.Text) && Line.Contains(tbPassword.Text))
                {
                    Exist = true;
                }
                Line = sr.ReadLine();
            }
            if (Exist)
            {
                Common.Login = tbLogin.Text; 
                Menu menu = new Menu();
                menu.LoginBox.Text = Common.Login;
                menu.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неправильные имя поотзователя или пароль");
            }
            sr.Close();
        }

        private void BReg_Click(object sender, RoutedEventArgs e)
        {
            bool Exist = false;
            StreamReader sr = new StreamReader("Auth.txt");
            string Line = sr.ReadLine();
            while (Line != null)
            {
                if (Line.Contains(tbLogin.Text))
                {
                    Exist = true;
                }
                Line = sr.ReadLine();
            }
            sr.Close();
            if (Exist)
            {
                MessageBox.Show("ПОльзователь '" + tbLogin.Text + "' уже зарегистрирован");
            }
            else
            {
                StreamWriter sw = new StreamWriter("Auth.txt", true);
                sw.WriteLine(tbLogin.Text + " " + tbPassword.Text);
                MessageBox.Show("Вы успешно зарегистрировались.");
                string TextVar = tbLogin.Text;
                sw.Close();
                Menu menu = new Menu();
                menu.Show();
                Close();
            }
        }

        private void bFirstExit_Click(object sender, RoutedEventArgs e)
        {
            Opacity = 1;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Start();         
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if ((Opacity -= 0.1)<=0)
            {
                Close();
            }
        }
    }
}
