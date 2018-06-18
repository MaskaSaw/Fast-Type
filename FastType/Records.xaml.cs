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
using System.IO;

namespace FastType
{
    /// <summary>
    /// Логика взаимодействия для Records.xaml
    /// </summary>
    public partial class Records : Window
    {
        int i = 0;
        string line = ""; 
        public Records()
        {
            InitializeComponent();
            StreamReader file = new StreamReader("records.txt");
            StreamReader namefile = new StreamReader("names.txt");
            while (i < 30 || file.ReadLine() != null)
            {
                if (i == 0)
                {
                    line = "Легкая сложность: ";
                    listbox1.Items.Add(line);
                }
                
                if (i < 10)
                {
                    line = file.ReadLine();
                    line = line +": "+namefile.ReadLine();
                    listbox1.Items.Add(line);
                    i += 1;
                }

                if (i == 10)
                {
                    line = "Средняя сложность: ";
                    listbox2.Items.Add(line);
                    line = file.ReadLine();
                    line = line + ": " + namefile.ReadLine();
                    listbox2.Items.Add(line);
                    i += 1;
                }

                if (i > 10 && i < 20)
                {
                    line = file.ReadLine();
                    line = line + ": " + namefile.ReadLine();
                    listbox2.Items.Add(line);
                    i += 1;
                }
                
                if (i == 20)
                {
                    line = "Высокая сложность: ";
                    listbox3.Items.Add(line);
                    line = file.ReadLine();
                    line = line + ": " + namefile.ReadLine();
                    listbox3.Items.Add(line);
                    i += 1;
                }

                if (i > 20 && i < 30)
                {
                    line = file.ReadLine();
                    line = line + ": " + namefile.ReadLine();
                    listbox3.Items.Add(line);
                    i += 1;
                }
            }
            file.Close();
            namefile.Close();
        }

        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.LoginBox.Text = Common.Login;
            menu.Show();
            Close();
        }
    }
}
