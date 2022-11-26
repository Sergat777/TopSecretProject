using GreatApparatusYebat.ProjectalesClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GreatApparatusYebat.Classes.ProjectalesClasses
{
    public enum ProjectileClass
    {
        Fireball,
        Arrow
    }

    public class ProjectileGenerator
    {
        public List<Projectile> GenerateProjectiles { get; set; } = new List<Projectile>();
        public ProjectileClass GenerateClass;
        public int ProjectilesPerInterval;
        public Directions Direction;
        public bool IsRandom;
        public Canvas GenerateArea;
        public bool IsON = false;

        private int _projectilesX;
        private int _projectilesY;
        private bool _projectilesToRight;
        private bool _projectilesToBottom;

        private DispatcherTimer _movingTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(30)};
        private DispatcherTimer _generateTimer = new DispatcherTimer();

        public ProjectileGenerator( ProjectileClass generateClass,
                                    int projectilesPerInterval,
                                    TimeSpan interval,
                                    Directions direction,
                                    bool isRandom,
                                    Canvas generateArea,
                                    int x = 0,
                                    int y = 0,
                                    bool toRight = true,
                                    bool toBottom = true)
        {
            GenerateClass = generateClass;
            ProjectilesPerInterval = projectilesPerInterval;
            Direction = direction;
            IsRandom = isRandom;
            GenerateArea = generateArea;

            _projectilesX = x;
            _projectilesY = y;
            _projectilesToRight = toRight;
            _projectilesToBottom = toBottom;

            _movingTimer.Tick += MoveProjectiles;
            _movingTimer.Start();

            _generateTimer.Interval = interval;
            _generateTimer.Tick += GenerateProjectile;
            _generateTimer.Start();

            IsON = true;
        }

        public void GenerateProjectile(object sender, EventArgs e)
        {
            if (IsON)
            {
                Random rndm = new Random();
                for (int i = 0; i < ProjectilesPerInterval; i++)
                {
                    if (IsRandom)
                    {
                        if (Direction == Directions.Mixed)
                        {
                            _projectilesToRight = Convert.ToBoolean(rndm.Next(0, 2));
                            _projectilesToBottom = Convert.ToBoolean(rndm.Next(0, 2));
                        }
                        else
                        {
                            _projectilesX = rndm.Next(0, (int)AppControls.MainCanvas.ActualWidth);
                            _projectilesY = rndm.Next(0, (int)AppControls.MainCanvas.ActualHeight);
                        }
                    }

                    if (GenerateClass == ProjectileClass.Arrow)
                    {
                        Arrow arrow = new Arrow(x: _projectilesX,
                                                y: _projectilesY,
                                                direction: Direction,
                                                toRight: _projectilesToRight,
                                                toBottom: _projectilesToBottom);

                        GenerateProjectiles.Add(arrow);
                        GenerateArea.Children.Add(arrow);
                    }

                    if (GenerateClass == ProjectileClass.Fireball)
                    {
                        Fireball fireball = new Fireball(x: _projectilesX,
                                                y: _projectilesY,
                                                direction: Direction);

                        GenerateProjectiles.Add(fireball);
                        GenerateArea.Children.Add(fireball);
                    }
                }
            }
        }

        public void MoveProjectiles(object sender, EventArgs e)
        {
            List<Projectile> RemovingList = new List<Projectile>();

            foreach (Projectile projectile in GenerateProjectiles)
            {
                projectile.Move();
                
                bool isOverX = Canvas.GetLeft(projectile) > AppControls.MainCanvas.ActualWidth
                                || Canvas.GetRight(projectile) > AppControls.MainCanvas.ActualWidth;

                bool isOverY = Canvas.GetTop(projectile) > AppControls.MainCanvas.ActualHeight
                                || Canvas.GetBottom(projectile) > AppControls.MainCanvas.ActualHeight;

                if (Direction == Directions.Mixed)
                {
                    if (isOverX && isOverY)
                    {
                        RemovingList.Add(projectile);
                    }
                }
                else if (isOverX || isOverY)
                {
                    RemovingList.Add(projectile);
                }
            }

            foreach (Projectile removingProjectile in RemovingList)
                AppControls.MainCanvas.Children.Remove(removingProjectile);
        }

        public void SwitchPower()
        {
            IsON = !IsON;
        }
    }
}
