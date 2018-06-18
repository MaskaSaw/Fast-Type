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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Threading;

namespace FastType
{
    /// <summary>
    /// Логика взаимодействия для Connection.xaml
    /// </summary>
    public partial class Connection : Window
    {
        private int Port = 10000;
        public static bool Pos { get; set; }
        private IPAddress localIp;
        private IPEndPoint localEP;
        private IPAddress remoteIp;
        private IPEndPoint remoteEP;
        private Thread receiveThread;
        private string message;

        public Connection()
        {
            Pos = true;
            InitializeComponent();
        }

        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            if (Pos)
            {
                Menu menu = new Menu();
                menu.LoginBox.Text = Common.Login;
                menu.Show();
                Close();
            }
            else
            {
                bHost.Visibility = Visibility.Visible;
                bClient.Visibility = Visibility.Visible;
                bConnect.Visibility = Visibility.Hidden;
                tbIP.Visibility = Visibility.Hidden;
                WaitBox.Visibility = Visibility.Hidden;
                tbIP.Text = "";
                PreviwBox.Text = "Многопользовательский режим";
                Pos = true;
            }
        }

        private void bHost_Click(object sender, RoutedEventArgs e)
        {
            WaitBox.Visibility = Visibility.Visible;
            bHost.Visibility = Visibility.Hidden;
            bClient.Visibility = Visibility.Hidden;
            Common.IsHost = true;
            SetLocalEP();
            receiveThread = new Thread(Receive);
            receiveThread.Start();
            Pos = false;
        }

        private void bClient_Click_1(object sender, RoutedEventArgs e)
        {
            bHost.Visibility = Visibility.Hidden;
            bClient.Visibility = Visibility.Hidden;
            bConnect.Visibility = Visibility.Visible;
            tbIP.Visibility = Visibility.Visible;
            Pos = false;
            Common.IsHost = false;
        }

        private void bConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                remoteIp = IPAddress.Parse(tbIP.Text);
                remoteEP = new IPEndPoint(remoteIp, Port);
                localIp = Dns.GetHostAddresses(Dns.GetHostName()).Where(x => x.AddressFamily == AddressFamily.InterNetwork).Last();
                Send(Common.Login, "/"+localIp.ToString());
                Multy multy = new Multy();
                multy.Player1Board.Text = Common.Player1;
                multy.Player2Board.Text = Common.Player2;
                multy.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        private void SetLocalEP()
        {
            try
            {
                // Установка конечной точки для принятия пакетов
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                localIp = Dns.GetHostAddresses(Dns.GetHostName()).Where(x => x.AddressFamily == AddressFamily.InterNetwork).Last();
                localEP = new IPEndPoint(localIp, Port);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            PreviwBox.Text = localIp.ToString();
        }

        private void Receive()
        {
            byte[] buffer = new byte[1024];

            try
            {
                using (Socket listener = new Socket(localIp.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                {
                    listener.Bind(localEP);
                    listener.Listen(5);
                    using (Socket socket = listener.Accept())
                    {
                        int count = 0;
                        count = socket.Receive(buffer);
                        message = Encoding.UTF8.GetString(buffer, 0, count);
                        socket.Shutdown(SocketShutdown.Both);

                        Common.IP = message.Substring(message.IndexOf("/")+1, message.Length - message.IndexOf("/") - 1);
                        message = message.Substring(0, message.IndexOf("/"));
                        Common.Player1 = Common.Login;
                        Common.Player2 = message;
                        Dispatcher.BeginInvoke(DispatcherPriority.Normal,(ThreadStart)delegate ()
                        {
                            MultyInvoke();
                        } );
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MultyInvoke()
        {
            Multy multy = new Multy();
            multy.Player1Board.Text = Common.Player1;
            multy.Player2Board.Text = Common.Player2;
            multy.Show();
            Close();
        }

        private void Send(string str, string enc_symb)
        {
            using (Socket socket = new Socket(remoteIp.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Connect(remoteEP);
                message = str + enc_symb;
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                try
                {
                    int bytes_sent = socket.Send(bytes);
                    socket.Shutdown(SocketShutdown.Both);
                    Common.IP = tbIP.Text;
                    Common.Player2 = Common.Login;
                    Common.Player1 = "user";
                }
                catch (SocketException e)
                {
                    MessageBox.Show(e.Message);
                    socket.Shutdown(SocketShutdown.Both);
                }        
            }
        }

    }
}
