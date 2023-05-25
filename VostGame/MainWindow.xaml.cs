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
using System.Windows.Threading;

namespace VostGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int tick;
        Ellipse Ball;
        double rad = 10;
        double bx, by;
        double speed = 3.5;
        double speedx, speedy;
        Rectangle Plate;
        double pw = 100;
        double px;
        double pv = 25;
        DispatcherTimer Timer;
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
            Ball = new Ellipse();
            Ball.Fill = Brushes.Red;
            Ball.Height = 2 * rad;
            Ball.Width = 2 * rad;
            Ball.Margin = new Thickness(bx, by, 0, 0);
            Field.Children.Add(Ball);

            Plate = new Rectangle();
            Plate.Fill = Brushes.Black;
            Plate.Height = 5;
            Plate.Width = pw;
            px = Field.Width / 2 - pw / 2;
            Plate.Margin = new Thickness(px, Field.Height, 0, 0);
            Field.Children.Add(Plate);

            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(onTick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            Timer.Start();
        }
        void NewGame()
        {
            bx = Field.Width / 2 - rad;
            by = Field.Height / 2 - rad;
            Random rnd = new Random();
            double way = rnd.NextDouble() * Math.PI / 2 + Math.PI / 2;
            speedx = speed * Math.Cos(way);
            speedy = speed * Math.Sin(way);
            px = Field.Width / 2 - pw / 2;
            tick = 0;
        }
        void onTick(object sender, EventArgs e)
        {
            tick++;
            if ((bx < 0) || (bx > Field.Width - 2 * rad))
            {
                speedx = -speedx;
            }
            if ((by<0)||(by>Field.Height - 2 * rad))
            {
                speedy = -speedy;
            }
            if (by > Field.Height - 2 * rad)
            {
                double xcord = bx + rad;
                if ((xcord >= px) && (xcord <= px + pw))
                {
                    speedx *= 1.1;
                    speedy *= 1.1;
                }
                else
                {
                    if (MessageBox.Show("Начать заново?", "Вы проиграли", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        NewGame();
                        Plate.Margin = new Thickness(px, Field.Height, 0, 0);
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
            bx += speedx;
            by += speedy;
            Ball.Margin = new Thickness(bx, by, 0, 0);
            TBTime.Text = (tick / 100).ToString();
        }
        private void pressKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                px -= pv;
            }
            if (e.Key == Key.Right)
            {
                px += pv;
            }
            if (px < 0)
            {
                px = 0;
            }
            if (px > Field.Width - pw)
            {
                px = Field.Width - pw;
            }
            Plate.Margin = new Thickness(px, Field.Height, 0, 0);
        }
    }
}
