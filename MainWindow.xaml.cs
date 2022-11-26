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

        public const UInt32 SPI_SETMOUSESPEED = 0x0071;

        [DllImport("User32.dll")]
        static extern Boolean SystemParametersInfo(
            UInt32 uiAction,
            UInt32 uiParam,
            UInt32 pvParam,
            UInt32 fWinIni);

        public MainWindow()
        {
            InitializeComponent();

            SystemParametersInfo(
                SPI_SETMOUSESPEED,
                0,
                0,
                0);

            AppControls.AppMainPanel = mainPanel;
            startTime = DateTime.Now.TimeOfDay;
            cnvsFightArea.Children.Add(player);
            AppControls.Player = player;
            AppControls.MainCanvas = cnvsFightArea;
            AppControls.HealthBar = barHealth;
            barHealth.Maximum = 20;
            generators.Add(new ProjectileGenerator(ProjectileClass.Fireball,
                                                   2, TimeSpan.FromMilliseconds(1000),
                                                   Directions.TopToBottom, true, cnvsFightArea));
            gameTimer.Tick += AddSecond;
            gameTimer.Start();

            MediaHelper.PlayMusic("romanticTechMusic");
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

            if (seconds == 7)
                generators.Add(new ProjectileGenerator(ProjectileClass.Arrow,
                                                       2, TimeSpan.FromMilliseconds(1000),
                                                       Directions.LeftToRight, true, cnvsFightArea));
            if (seconds == 15)
                generators.Add(new ProjectileGenerator(ProjectileClass.Arrow,
                                                       2, TimeSpan.FromMilliseconds(1000),
                                                       Directions.RightToLeft, true, cnvsFightArea));
            if (seconds == 30)
                generators.Add(new ProjectileGenerator(ProjectileClass.Arrow,
                                                       2, TimeSpan.FromMilliseconds(1500),
                                                       Directions.Mixed, true, cnvsFightArea));
            if (seconds == 45)
                generators.Add(new ProjectileGenerator(ProjectileClass.Arrow,
                                                       2, TimeSpan.FromMilliseconds(1500),
                                                       Directions.BottomToTop, true, cnvsFightArea));
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
