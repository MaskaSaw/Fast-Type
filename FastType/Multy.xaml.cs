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
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FastType
{
    /// <summary>
    /// Логика взаимодействия для Multy.xaml
    /// </summary>
    public partial class Multy : Window
    {
        const int Port1 = 11000;

        public static int Margin_Letter { get; set; }
        public static int Speed { get; set; }
        public static int next { get; set; }
        public static int Interval { get; set; }
        public static int Counter { get; set; }
        public static int CurrentInterval { get; set; }
        public static int CurrentSpeed { get; set; }

        public String[] NormArr = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        public static int Score1 { get; set; }
        public static int Score2 { get; set; }
        public static int PlayTime { get; set; }
        public static int Combo1 { get; set; }
        public static int Combo2 { get; set; }
        public static int k { get; set; }
        public static bool Way { get; set; }

        public bool Player1Start = false;
        public bool Player2Start = false;

        Random rnd = new Random();
        public DispatcherTimer timerMove;
        public DispatcherTimer timerPlay;
        public DispatcherTimer timerPic;

        private IPAddress localIp;
        private IPEndPoint localEP;
        private IPAddress remoteIp;
        private IPEndPoint remoteEP;


        private Thread receiveThread;
        private string message;

        private void InitMoveTimer()
        {
            timerMove = new DispatcherTimer();
            timerMove.Interval = new TimeSpan(0, 0, 0, 0, Interval);
            timerMove.Tick += new EventHandler(Move);
            timerMove.Start();
        }

        public void InitPlayTimer()
        {
            timerPlay = new DispatcherTimer();
            timerPlay.Interval = new TimeSpan(0, 0, 0, 1);
            timerPlay.Tick += new EventHandler(Play);
            timerPlay.Start();
        }

        public void InitPicTimer1()
        {
            Way = true;
            k = 90;
            timerPic = new DispatcherTimer();
            timerPic.Interval = new TimeSpan(0, 0, 0, 0, 5);
            timerPic.Tick += new EventHandler(Pic1);
            timerPic.Start();
        }

        public void InitPicTimer2()
        {
            Way = true;
            k = 90;
            timerPic = new DispatcherTimer();
            timerPic.Interval = new TimeSpan(0, 0, 0, 0, 5);
            timerPic.Tick += new EventHandler(Pic2);
            timerPic.Start();
        }

        public Multy()
        {
            InitializeComponent();
            SetLocalEP();
            if (Common.IsHost)
            {
                remoteIp = IPAddress.Parse(Common.IP);
                remoteEP = new IPEndPoint(remoteIp, Port1);
            }
            else
            {
                remoteIp = IPAddress.Parse(Common.IP);
                remoteEP = new IPEndPoint(remoteIp, Port1);
            }
            receiveThread = new Thread(Receive);
            receiveThread.Start();
        }

        private void SetLocalEP()
        {
            try
            {
                // Установка конечной точки для принятия пакетов
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                localIp = Dns.GetHostAddresses(Dns.GetHostName()).Where(x => x.AddressFamily == AddressFamily.InterNetwork).Last();
                if (Common.IsHost)
                {
                    localEP = new IPEndPoint(localIp, Port1);
                }
                else
                {
                    localEP = new IPEndPoint(localIp, Port1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Send(string msg, string pref)
        {
            using (Socket socket = new Socket(remoteIp.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Connect(remoteEP);
                message = msg + pref;
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                try
                {
                    int bytes_sent = socket.Send(bytes);
                }
                catch (SocketException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private void Receive()
        {
            byte[] buffer = new byte[1024];
            while (true)
            {
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
                            if (message.Contains("/s") && message.Contains("1"))
                            {
                                    Player1Start = true;
                                Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                                {
                                    CheckStart();
                                });
                            }

                            if (message.Contains("/s") && message.Contains("2"))
                            {
                                Player2Start = true;
                                Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                                {
                                    CheckStart();
                                });
                            }

                            if (message.Contains("/l"))
                            {
                                if(message.Contains(NormArr[next]))
                                {
                                    if (Common.IsHost)
                                    {
                                        Combo2++;
                                        Score2 += Combo2 * 2;
                                        Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                                        {
                                            ScoreBox2.Text = Score2.ToString();
                                            ComboBox2.Text = Combo2.ToString();
                                            Margin_Letter = 0;
                                            InitPicTimer2();
                                            LetterBox.Text = NormArr[GenerateNext()];
                                            EnemyImg.Margin = new Thickness(0, 0, 0, 350);
                                        });
                                    }
                                    else
                                    {
                                        Combo1++;
                                        Score1 += Combo1 * 2;
                                        Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                                        {
                                            ScoreBox1.Text = Score1.ToString();
                                            ComboBox1.Text = Combo1.ToString();
                                            Margin_Letter = 0;
                                            InitPicTimer1();
                                            LetterBox.Text = NormArr[GenerateNext()]; 
                                            EnemyImg.Margin = new Thickness(0, 0, 0, 350);
                                        });
                                    }
                                }
                                else
                                {
                                    if (Common.IsHost)
                                    {
                                        Combo2 = 0;
                                        Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                                        {
                                            ComboBox2.Text = Combo2.ToString();
                                        });
                                    }
                                    else
                                    {
                                        Combo1 = 0;
                                        Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                                        {
                                            ComboBox1.Text = Combo1.ToString();
                                        });
                                    }
                                }                               
                            }  
                            
                            if (message.Contains("/p"))
                            {
                                Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                                {
                                    Margin_Letter = 0;
                                    timerMove.Stop();
                                    timerPlay.Stop();
                                    TimeBox.Text = "";
                                    LetterBox.Text = "";
                                    EnemyImg.Visibility = Visibility.Hidden;
                                    LetterBox.Visibility = Visibility.Hidden;
                                    EnemyImg.Margin = new Thickness(0, 0, 0, 350);
                                    Player1Start = false;
                                    Player2Start = false;
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }    
        }

        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.LoginBox.Text = Common.Login;
            menu.Show();
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Send(e.Key.ToString(), "/l");
            if (Common.IsHost)
            {
                if (e.Key.ToString().Contains(LetterBox.Text))
                {
                    Combo1++;
                    ComboBox1.Text = Combo1.ToString();
                    Score1 += 2 * Combo1;
                    ScoreBox1.Text = Score1.ToString();
                    Margin_Letter = 0;
                    InitPicTimer1();
                    LetterBox.Text = NormArr[GenerateNext()];       
                    EnemyImg.Margin = new Thickness(0, 0, 0, 350);
                }
                else
                {
                    Combo1 = 0;
                    ComboBox1.Text = Combo1.ToString();
                }
            }
            else
            {
                if (e.Key.ToString().Contains(LetterBox.Text))
                {
                    Combo2++;
                    ComboBox2.Text = Combo2.ToString();
                    Score2 += 2 * Combo2;
                    ScoreBox2.Text = Score2.ToString();
                    InitPicTimer2();
                    Margin_Letter = 0;
                    LetterBox.Text = NormArr[GenerateNext()];
                    EnemyImg.Margin = new Thickness(0, 0, 0, 350);
                }
                else
                {
                    Combo2 = 0;
                    ComboBox2.Text = Combo2.ToString();
                }
            }
          
        }

        private void bStart_Click(object sender, RoutedEventArgs e)
        {
            if (Common.IsHost)
            {
                Player1Start = true;
                Send("1", "/s");
            }
            else
            {
                Player2Start = true;
                Send("2", "/s");
            }
           
            CheckStart();
        }

        private void CheckStart()
        {
            if (Player1Start == true && Player2Start == true)
            {
                Speed = 3;
                Interval = 1;
                EnemyImg.Visibility = Visibility.Visible;
                next = 1;
                ScoreBox1.Text = "0";
                ComboBox1.Text = "0";
                Score1 = 0;
                Combo1 = 0;
                ScoreBox2.Text = "0";
                ComboBox2.Text = "0";
                Score2 = 0;
                Combo2 = 0;
                LetterBox.Text = NormArr[next];
                TimeBox.Text = "60";
                PlayTime = 60;
                Counter = 3;
                InitMoveTimer();
                InitPlayTimer();
            }
        }


        private void bStop_Click(object sender, RoutedEventArgs e)
        {
            Send("", "/p");
            Margin_Letter = 0;
            timerMove.Stop();
            timerPlay.Stop();
            TimeBox.Text = "";
            LetterBox.Text = "";
            if (Common.IsHost)
            {
                MessageBox.Show("Поздравляем, вы набрали: " + Score1.ToString() + " очков");
                MessageBox.Show("Ваш противник набрал: " + Score2.ToString() + " очков");
            }
            else
            {
                MessageBox.Show("Поздравляем, вы набрали: " + Score2.ToString() + " очков");
                MessageBox.Show("Ваш противник набрал: " + Score1.ToString() + " очков");
            }
            EnemyImg.Visibility = Visibility.Hidden;
            LetterBox.Visibility = Visibility.Hidden;
            EnemyImg.Margin = new Thickness(0, 0, 0, 350);
        }

        private void Move(object sender, EventArgs e)
        {
            Margin_Letter += Speed;
            EnemyImg.Margin = new Thickness(0, 0, Margin_Letter, 350);
            if (Margin_Letter > 1320)
            {
                Margin_Letter = 0;
                Combo1 = 0;
                Combo2 = 0;
                EnemyImg.Margin = new Thickness(0, 0, 0, 350);
            }
        }

        private void Pic1(object sender, EventArgs e)
        {
            if (Way)
            {
                k = k + 40;
                if (k < 350)
                    PlayerImg1.Margin = new Thickness(40, 0, 0, k);
                else
                    Way = false;
            }
            else
            {
                k = k - 50;
                if (k > 80)
                    PlayerImg1.Margin = new Thickness(40, 0, 0, k);
                else
                {
                    PlayerImg1.Margin = new Thickness(40, 0, 0, 80);
                    timerPic.Stop();
                }
            }
        }

        private void Pic2(object sender, EventArgs e)
        {
            if (Way)
            {
                k = k + 40;
                if (k < 350)
                    PlayerImg2.Margin = new Thickness(140, 0, 0, k);
                else
                    Way = false;
            }
            else
            {
                k = k - 50;
                if (k > 80)
                    PlayerImg2.Margin = new Thickness(140, 0, 0, k);
                else
                {
                    PlayerImg2.Margin = new Thickness(140, 0, 0, 80);
                    timerPic.Stop();
                }
            }
        }

        private void Play(object sender, EventArgs e)
        {
            PlayTime -= 1;
            TimeBox.Text = PlayTime.ToString();
            if (PlayTime < 0)
            {
                timerMove.Stop();
                timerPlay.Stop();
                TimeBox.Text = "";
                LetterBox.Text = "";
                if (Common.IsHost)
                {
                    MessageBox.Show("Поздравляем, вы набрали: " + Score1.ToString() + " очков");
                    MessageBox.Show("Ваш противник набрал: " + Score2.ToString() + " очков");
                }
                else
                {
                    MessageBox.Show("Поздравляем, вы набрали: " + Score2.ToString() + " очков");
                    MessageBox.Show("Ваш противник набрал: " + Score1.ToString() + " очков");
                }
            }
        }

        private int GenerateNext()
        {
            next = (next + 3) % 36;
            if (next == Counter)
            {
                next++;
                Counter++;
            }
                
            ;
            return next;
        }

    }
}
