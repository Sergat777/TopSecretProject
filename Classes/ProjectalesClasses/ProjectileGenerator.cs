﻿using GreatApparatusYebat.ProjectalesClasses;
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
        Directions Direction;
        bool IsRandom;
        Canvas GenerateArea;

        private int _projectilesX;
        private int _projectilesY;
        private bool _projectilesToRight;
        private bool _projectilesToBottom;

        DispatcherTimer movingTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(30)};
        DispatcherTimer generateTimer = new DispatcherTimer();

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

            movingTimer.Tick += MoveProjectiles;
            movingTimer.Start();

            generateTimer.Interval = interval;
            generateTimer.Tick += GenerateProjectile;
            generateTimer.Start();
        }

        public void GenerateProjectile(object sender, EventArgs e)
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
            }

            //MessageBox.Show(AppControls.MainCanvas.Children.Count.ToString());
        }

        public void MoveProjectiles(object sender, EventArgs e)
        {
            AppControls.MainCanvas.Children.RemoveRange(1, AppControls.MainCanvas.Children.Count - 1);
            foreach (Projectile projectile in GenerateProjectiles)
            {
                AppControls.MainCanvas.Children.Add(projectile);
                projectile.Move();
            }

            List<Projectile> RemovingList = new List<Projectile>();

            foreach (Projectile projectile in AppControls.MainCanvas.Children.OfType<Projectile>())
            {
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

            foreach (Projectile a in RemovingList)
                GenerateProjectiles.Remove(a);
        }
    }
}
