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
            new ProjectileGenerator(ProjectileClass.Fireball, 6, TimeSpan.FromMilliseconds(1500),
                                    Directions.LeftToRight, true, cnvsFightArea);
            gameTimer.Tick += AddSecond;
            gameTimer.Start();

            //MediaHelper.PlayMusic("fastTechMusic");
        }

        DispatcherTimer gameTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1)};

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

            if (barHealth.Value == 0)
            {
                MessageBox.Show("Заебався? ");
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
