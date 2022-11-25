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

namespace GreatApparatusYebat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Heart player = new Heart();
        TimeSpan startTime;

        public MainWindow()
        {
            InitializeComponent();

            AppControls.AppMainPanel = mainPanel;
            startTime = DateTime.Now.TimeOfDay;
            cnvsFightArea.Children.Add(player);
            AppControls.Player = player;
            AppControls.MainCanvas = cnvsFightArea;
            AppControls.HealthBar = barHealth;
            barHealth.Maximum = 20;

            movingTimer.Tick += MoveProjectile;
            movingTimer.Start();

            creatingTimer.Tick += CreateProjectile;
            creatingTimer.Start();

            gameTime.Tick += AddSecond;
            gameTime.Start();

            MediaHelper.PlayMusic("fastTechMusic");
        }

        DispatcherTimer gameTime = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1)};
        DispatcherTimer movingTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(30) };
        DispatcherTimer creatingTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(1000) };

        Random rndm = new Random();

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
        }

        private void CreateProjectile(object sender, EventArgs e)
        {
            if ((DateTime.Now.TimeOfDay - startTime).Seconds < 7)
            {
                Arrow arrow0 = new Arrow(direction: StraightDirections.LeftToRight,
                                        y: (int)cnvsFightArea.ActualHeight - 30);
                                                  
                //Arrow arrow1 = new Arrow(y: rndm.Next(0, (int)cnvsFightArea.ActualHeight), direction: StraightDirections.RightToLeft);
                //Arrow arrow2 = new Arrow(x: rndm.Next(0, (int)cnvsFightArea.ActualWidth), direction: StraightDirections.TopToBottom);
                //Arrow arrow3 = new Arrow(x: rndm.Next(0, (int)cnvsFightArea.ActualWidth), direction: StraightDirections.BottomToTop);
                cnvsFightArea.Children.Add(arrow0);
                //cnvsFightArea.Children.Add(arrow1);
                //cnvsFightArea.Children.Add(arrow2);
                //cnvsFightArea.Children.Add(arrow3);
            }
            else
                if ((DateTime.Now.TimeOfDay - startTime).Seconds > 10)
            {
                MessageBox.Show(cnvsFightArea.Children.Count.ToString());
                cnvsFightArea.Children.RemoveRange(1, cnvsFightArea.Children.Count - 1);
                MessageBox.Show(cnvsFightArea.Children.Count.ToString());
            }
            //Arrow arrowRight = new Arrow(direction: StraightDirections.RightToLeft,
            //                            y: (int)cnvsFightArea.ActualHeight - 3);
            //Arrow arrowLeft = new Arrow(direction: StraightDirections.LeftToRight);
            //Arrow arrowBottom = new Arrow(direction: StraightDirections.TopToBottom);
            //Arrow arrowTop = new Arrow(direction: StraightDirections.BottomToTop,
            //                            x: (int)cnvsFightArea.ActualWidth - 30);

            //cnvsFightArea.Children.Add(arrowBottom);
            //cnvsFightArea.Children.Add(arrowTop);
            //cnvsFightArea.Children.Add(arrowLeft);
            //cnvsFightArea.Children.Add(arrowRight);
        }

        private void MoveProjectile(object sender, EventArgs e)
        {
            
            foreach (Projectile projectile in cnvsFightArea.Children.OfType<Projectile>())
            {
                projectile.Move();
            }

            if (barHealth.Value == 0)
            {
                MessageBox.Show("Заебався? " + cnvsFightArea.Children.Count);
                Close();
            }
            txtHealth.Text = barHealth.Value + "/20";
        }

        private void cnvsFightArea_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePosition = Mouse.GetPosition(cnvsFightArea);
            Canvas.SetLeft(player, mousePosition.X);
            Canvas.SetTop(player, mousePosition.Y);
        }

        private void brdFight_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
