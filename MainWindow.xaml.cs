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


            cnvsFightArea.Children.Add(player);
            AppControls.Player = player;
            AppControls.MainCanvas = cnvsFightArea;
            AppControls.HealthBar = barHealth;
            barHealth.Maximum = 30;

            movingTimer.Tick += MoveProjectile;
            movingTimer.Start();

            creatingTimer.Tick += CreateProjectile;
            creatingTimer.Start();

        }

        DispatcherTimer movingTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(30) };
        DispatcherTimer creatingTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(777) };

        private void CreateProjectile(object sender, EventArgs e)
        {
            cnvsFightArea.Children.Add(new Fireball());
            cnvsFightArea.Children.Add(new Fireball(toBottom: false));
            cnvsFightArea.Children.Add(new Fireball(y: (int)cnvsFightArea.ActualHeight / 2));
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
