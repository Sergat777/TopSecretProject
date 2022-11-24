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

        public MainWindow()
        {
            InitializeComponent();

            AppControls.AppMainPanel = mainPanel;

            cnvsFightArea.Children.Add(player);
            AppControls.Player = player;
            AppControls.MainCanvas = cnvsFightArea;
            AppControls.HealthBar = barHealth;
            barHealth.Maximum = 20;

            movingTimer.Tick += MoveProjectile;
            movingTimer.Start();

            creatingTimer.Tick += CreateProjectile;
            creatingTimer.Start();

            MediaHelper.PlayMusic("fastTechMusic");
        }

        DispatcherTimer movingTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(30) };
        DispatcherTimer creatingTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(1000) };

        Random rndm = new Random();

        private void CreateProjectile(object sender, EventArgs e)
        {
            for (int i = 0; i < 7; i++)
            {

                if (i % 2 != 0)
                {
                    Arrow projectile = new Arrow(y: rndm.Next(0, (int)cnvsFightArea.ActualHeight),
                                                toRight: Convert.ToBoolean(rndm.Next(0, 2)),
                                                toBottom: Convert.ToBoolean(rndm.Next(0, 2)));
                    cnvsFightArea.Children.Add(projectile);
                }
                else
                {
                    Fireball projectile = new Fireball(y: rndm.Next(0, (int)cnvsFightArea.ActualHeight),
                                                toRight: Convert.ToBoolean(rndm.Next(0, 2)));
                    cnvsFightArea.Children.Add(projectile);
                }

            }
        }

        private void MoveProjectile(object sender, EventArgs e)
        {
            foreach (Projectile projectile in cnvsFightArea.Children.OfType<Projectile>())
            {
                projectile.Move();
            }
            txtHealth.Text = barHealth.Value + "/30";
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
