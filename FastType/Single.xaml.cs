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
using System.Threading;
using System.IO;

namespace FastType
{
    /// <summary>
    /// Логика взаимодействия для Single.xaml
    /// </summary>
    public partial class Single : Window
    {
        const int EzSpeed = 2;
        const int MediumSpeed = 3;
        const int HardSpeed = 4;
        const int EzInterval = 2;
        const int HardInterval = 1;

        public static int Margin_Letter { get; set; }
        public static int Speed { get; set; }
        public static int Interval { get; set; }
        public static int CurrentInterval { get; set; }
        public static int next { get; set; }
        public static int CurrentSpeed { get; set; }

        public string[] EzArr = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        public string[] NormArr = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" ,"1","2", "3", "4", "5", "6", "7", "8", "9", "0"};
        public string[] SelectedArray;
        public string[] RecordsArr = new String[30];
        public string[] NamesArr = new String[30];
        public static int SelectedArrayIndex { get; set; }
        public static int Score { get; set; }
        public static int PlayTime { get; set; }
        public static int Combo { get; set; }
        public static int Controller { get; set; }
        public static int Ctrl { get; set; }
        public static int k { get; set; }
        public static bool Way { get; set; }

        Random rnd = new Random();
        public DispatcherTimer timerMove;
        public DispatcherTimer timerPlay;
        public DispatcherTimer timerPic;

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

        public void InitPicTimer()
        {
            Way = true;
            k = 90;
            timerPic = new DispatcherTimer();
            timerPic.Interval = new TimeSpan(0, 0, 0, 0, 5);
            timerPic.Tick += new EventHandler(Pic);
            timerPic.Start();
        }

        public Single()
        {
            Margin_Letter = 0;
            InitializeComponent();
        }

        private void BBack_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.LoginBox.Text = Common.Login;
            menu.Show();
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString().Contains(LetterBox.Text) )
            {
                Combo++;
                ComboBox.Text = Combo.ToString();
                Score += 2 * Combo;
                Controller += 2 * Combo;
                Ctrl += 2 * Combo;
                ScoreBox.Text = Score.ToString();
                InitPicTimer();
                if (Ctrl / 200 > 1)
                {
                    Speed += 1;
                    Ctrl = Ctrl % 200;
                }
                Margin_Letter = 0;
                next = rnd.Next(0, SelectedArrayIndex);
                LetterBox.Text = SelectedArray[next];
                EnemyImg.Margin = new Thickness(0, 0, 0, 350);
            }
            else
            {
                Combo = 0;
                ComboBox.Text = Combo.ToString();
                Speed = CurrentSpeed;
                Interval = CurrentInterval;
            }
        }

        private void bStart_Click(object sender, RoutedEventArgs e)
        {
            InitComponents();
            Speed = CurrentSpeed;
            Interval = CurrentInterval;
            EnemyImg.Visibility = Visibility.Visible;
            next = rnd.Next(0, SelectedArrayIndex);
            ScoreBox.Text = "0";
            ComboBox.Text = "0";
            Score = 0;
            Combo = 0;
            Controller = 0;
            LetterBox.Text = SelectedArray[next];
            TimeBox.Text = "60";
            PlayTime = 60;
            InitMoveTimer();
            InitPlayTimer();
        }

        private void InitComponents()
        {
            if (DifEz.IsChecked == true)
            {
                SelectedArray = EzArr;
                SelectedArrayIndex = 26;
                CurrentSpeed = EzSpeed;
                CurrentInterval = EzInterval;
            }

            if (DifSt.IsChecked == true)
            {
                SelectedArray = NormArr;
                SelectedArrayIndex = 36;
                CurrentSpeed = MediumSpeed;
                CurrentInterval = EzInterval;
                Interval = 2;
            }

            if (DifHard.IsChecked == true)
            {
                SelectedArray = NormArr;
                SelectedArrayIndex = 36;
                CurrentSpeed = HardSpeed;
                CurrentInterval = HardInterval;
            }
        }

        private void bStop_Click(object sender, RoutedEventArgs e)
        {
            Margin_Letter = 0;
            timerMove.Stop();
            timerPlay.Stop();
            TimeBox.Text = "";
            LetterBox.Text = "";
            MessageBox.Show("Поздравляем, вы набрали: " + Score.ToString() + " очков");
            WriteRecords();
            EnemyImg.Visibility = Visibility.Hidden;
            LetterBox.Visibility = Visibility.Hidden;
            EnemyImg.Margin = new Thickness(0, 0, 0, 350);

        }

        private void Move(object sender, EventArgs e)
        {
            Margin_Letter += Speed;
            EnemyImg.Margin = new Thickness(0, 0, Margin_Letter, 350);
            if  (Margin_Letter >= 1320)
            {
                EnemyImg.Margin = new Thickness(0, 0, 0, 350);
                Combo = 0;
                Speed = CurrentSpeed;
            }
        }

        private void Pic(object sender, EventArgs e)
        {
            if (Way)
            {
                k = k + 40;
                if (k < 350)
                    PlayerImg.Margin = new Thickness(40, 0, 0, k);
                else
                    Way = false;
            }
            else
            {
                k = k - 50;
                if (k > 80)
                    PlayerImg.Margin = new Thickness(40, 0, 0, k);
                else
                {
                    PlayerImg.Margin = new Thickness(40, 0, 0, 80);
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
                MessageBox.Show("Поздравляем, вы набрали: " + Score.ToString() + " очков");

                WriteRecords();
            }
        }

        private void WriteRecords()
        {
            int i = 0;
            StreamReader file = new StreamReader("records.txt");
            while (i < 30)
            {
                RecordsArr[i] = file.ReadLine();
                if (RecordsArr[i] == null)
                    break;
                i++;
            }
            i = 0;
            StreamReader namefile = new StreamReader("names.txt");
            while (i < 30)
            {
                NamesArr[i] = namefile.ReadLine();
                if (NamesArr[i] == null)
                    break;
                i++;
            }

            file.Close();
            namefile.Close();
            if (DifEz.IsChecked == true)
            {
                i = 9;
                while (i >= 0)
                {
                    if (Score >= Convert.ToInt32(RecordsArr[i]) && Score < Convert.ToInt32(RecordsArr[i]))
                    {
                        RecordsArr[i] = Score.ToString();
                        NamesArr[i] = Common.Login;
                        break;
                    }
                    if (Score >= Convert.ToInt32(RecordsArr[0]))
                    {
                        RecordsArr[0] = Score.ToString();
                        NamesArr[0] = Common.Login;
                        break;
                    }
                    i--;
                }
            }

            if (DifSt.IsChecked == true)
            {
                i = 19;
                while (i >= 10)
                {
                    if (Score >= Convert.ToInt32(RecordsArr[i]) && Score < Convert.ToInt32(RecordsArr[i]))
                    {
                        RecordsArr[i] = Score.ToString();
                        NamesArr[i] = Common.Login;
                        break;
                    }
                    if (Score >= Convert.ToInt32(RecordsArr[10]))
                    {
                        RecordsArr[10] = Score.ToString();
                        NamesArr[10] = Common.Login;
                        break;
                    }
                    i--;
                }
            }

            if (DifHard.IsChecked == true)
            {
                i = 29;
                while (i >= 20)
                {
                    if (Score >= Convert.ToInt32(RecordsArr[i]) && Score < Convert.ToInt32(RecordsArr[i]))
                    {
                        RecordsArr[i] = Score.ToString();
                        NamesArr[i] = Common.Login;
                        break;
                    }
                    if (Score >= Convert.ToInt32(RecordsArr[20]))
                    {
                        RecordsArr[20] = Score.ToString();
                        NamesArr[20] = Common.Login;
                        break;
                    }
                    i--;
                }
            }
            StreamWriter sw = new StreamWriter("records.txt", false, System.Text.Encoding.Default);
            StreamWriter sw1 = new StreamWriter("names.txt", false, System.Text.Encoding.Default);

            for (i = 0; i < 30; i++)
            {
                sw.WriteLine(RecordsArr[i].ToString());
                sw1.WriteLine(NamesArr[i]);
            }

            sw.Close();
            sw1.Close();
        }
    }
}
