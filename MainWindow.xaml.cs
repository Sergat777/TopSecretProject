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

namespace GreatApparatusYebat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            movingTimer.Tick += MoveProjectile;
            movingTimer.Start();

            creatingTimer.Tick += CreateProjectile;
            creatingTimer.Start();
        }

        DispatcherTimer movingTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(30) };
        DispatcherTimer creatingTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(777) };

        private void CreateProjectile(object sender, EventArgs e)
        {

        }

        private void MoveProjectile(object sender, EventArgs e)
        {
            Canvas.SetLeft(Enemy, Canvas.GetLeft(Enemy) + 3);
        }

        private void cnvsFightArea_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePosition = Mouse.GetPosition(cnvsFightArea);
            Canvas.SetLeft(rectangleHero, mousePosition.X);
            Canvas.SetTop(rectangleHero, mousePosition.Y);
        }

        private void brdFight_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
