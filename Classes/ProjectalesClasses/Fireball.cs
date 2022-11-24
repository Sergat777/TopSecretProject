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
        public Fireball(    int height = 30,
                            int width = 30,
                            bool toRight = true,
                            bool toBottom = true,
                            int x = 0,
                            int y = 0) : base (height, width, toRight, toBottom, x, y)
        {
            Source = MediaHelper.GetBitmapImage("Fireball\\0");
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
