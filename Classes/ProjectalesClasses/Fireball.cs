using GreatApparatusYebat.ProjectalesClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace GreatApparatusYebat.Classes.ProjectalesClasses
{
    public class Fireball : Projectile
    {
        public Fireball(int height = 30,
                        int width = 30,
                        int x = 0,
                        int y = 0,
                        StraightDirections direction = StraightDirections.Mixed,
                        bool toRight = true,
                        bool toBottom = true) : base (height, width, x, y, direction, toRight, toBottom)
        {
            Source = MediaHelper.GetBitmapImage("Fireball\\0");
            Width = width;
            Height = width / 2;
        }

        public override void Move()
        {
            if (ToRight)
                Canvas.SetLeft(this, Canvas.GetLeft(this) + Speed);
            else
                Canvas.SetRight(this, Canvas.GetRight(this) + Speed);

            UpdateProjectileRect();
            CheckIntersectWithHero();
        }
    }
}
