using GreatApparatusYebat.ProjectalesClasses;
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
using GreatApparatusYebat.Classes.ProjectalesClasses;
using GreatApparatusYebat.Classes;
using System.Runtime.InteropServices;

namespace GreatApparatusYebat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Heart player = new Heart();
        List<ProjectileGenerator> generators = new List<ProjectileGenerator>();
        TimeSpan startTime;
        DispatcherTimer gameTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };

        public MainWindow()
        {
            InitializeComponent();
            AppControls.AppMainPanel = mainPanel;
            startTime = DateTime.Now.TimeOfDay;
            cnvsFightArea.Children.Add(player);
            AppControls.Player = player;
            Canvas.SetLeft(player, 0);
            Canvas.SetTop(player, 0);
            AppControls.MainCanvas = cnvsFightArea;
            AppControls.HealthBar = barHealth;
            barHealth.Maximum = 20;
            generators.Add(new ProjectileGenerator(ProjectileClass.Arrow,
                                                   3, TimeSpan.FromMilliseconds(1500),
                                                   Directions.RightToLeft, true, cnvsFightArea));
            generators.Add(new ProjectileGenerator(ProjectileClass.Arrow,
                                                   3, TimeSpan.FromMilliseconds(1500),
                                                   Directions.BottomToTop, true, cnvsFightArea));
            gameTimer.Tick += AddSecond;
            gameTimer.Start();
            _keyboardTimer.Tick += HeartMove;
           // MediaHelper.PlayMusic("romanticTechMusic");
        }

        private void AddSecond(object sender, EventArgs e)
        {
            byte minutes = (byte)(DateTime.Now.TimeOfDay - startTime).Minutes;
            byte seconds = (byte)(DateTime.Now.TimeOfDay - startTime).Seconds;
            txtTime.Text = "";

            if (minutes < 10)
                txtTime.Text += "0" + minutes;
            else
                txtTime.Text += minutes;

            txtTime.Text += ":";

            if (seconds < 10)
                txtTime.Text += "0" + seconds;
            else
                txtTime.Text += seconds;

            txtTime.Text += "\n" + cnvsFightArea.Children.Count;

            //if (seconds == 7)
            //    generators.Add(new ProjectileGenerator(ProjectileClass.Arrow,
            //                                           2, TimeSpan.FromMilliseconds(1000),
            //                                           Directions.LeftToRight, true, cnvsFightArea));
            //if (seconds == 15)
            //    generators.Add(new ProjectileGenerator(ProjectileClass.Arrow,
            //                                           2, TimeSpan.FromMilliseconds(1000),
            //                                           Directions.RightToLeft, true, cnvsFightArea));
            //if (seconds == 30)
            //    generators.Add(new ProjectileGenerator(ProjectileClass.Arrow,
            //                                           2, TimeSpan.FromMilliseconds(1500),
            //                                           Directions.Mixed, true, cnvsFightArea));
            //if (seconds == 45)
            //    generators.Add(new ProjectileGenerator(ProjectileClass.Arrow,
            //                                           2, TimeSpan.FromMilliseconds(1500),
            //                                           Directions.BottomToTop, true, cnvsFightArea));
            if (seconds == 0)
            {
                foreach (ProjectileGenerator generator in generators)
                    generator.SwitchPower();
                generators.Clear();
            }

            txtHealth.Text = barHealth.Value + "/20";
            if (barHealth.Value == 0)
            {
                MessageBox.Show("Ты проебался. . .");
                Close();
            }
        }

        private void cnvsFightArea_MouseMove(object sender, MouseEventArgs e)
        {
            //Point mousePosition = Mouse.GetPosition(cnvsFightArea);
            //Canvas.SetLeft(player, mousePosition.X);
            //Canvas.SetTop(player, mousePosition.Y);
        }

        private void brdFight_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        DispatcherTimer _keyboardTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(30) };

        
        //Key CurrentKey;
        //Key PreviousKey;
        bool ToUp = false;
        bool ToDown = false;
        bool ToLeft = false;
        bool ToRight = false;


        public void HeartMove(object sender, EventArgs e)
        {
            if (ToLeft)
                player.MoveToLeft();

            if (ToRight)
                player.MoveToRight();

            if (ToUp)
                player.MoveToUp();

            if (ToDown)
                player.MoveToDown();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.A)
                ToLeft = true;

            if (e.Key == Key.Right || e.Key == Key.D)
                ToRight = true;

            if (e.Key == Key.Up || e.Key == Key.W)
                ToUp = true;

            if (e.Key == Key.Down || e.Key == Key.S)
                ToDown = true;

            //if (CurrentKey != e.Key && PreviousKey != e.Key)
            //{
            //    HeartMove(sender, e);
            //    PreviousKey = CurrentKey;
            //    CurrentKey = e.Key;
            //}
            _keyboardTimer.Start();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.A)
                ToLeft = false;

            if (e.Key == Key.Right || e.Key == Key.D)
                ToRight = false;

            if (e.Key == Key.Up || e.Key == Key.W)
                ToUp = false;

            if (e.Key == Key.Down || e.Key == Key.S)
                ToDown = false;

            if (!ToLeft && !ToRight && !ToUp && !ToDown)
                _keyboardTimer.Stop();

            //if (CurrentKey == e.Key)
            //{
            //    CurrentKey = Key.Clear;
            //}
            //if (PreviousKey == e.Key)
            //{
            //    CurrentKey = Key.Clear;
            //    PreviousKey = CurrentKey;
            //}
            //if (CurrentKey == Key.Clear &&
            //    PreviousKey == Key.Clear)
            //    _keyboardTimer.Stop();
        }
    }
}
