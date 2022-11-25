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
        public Fireball(int height = 45,
                        int width = 45,
                        int x = 0,
                        int y = 0,
                        Directions direction = Directions.Mixed) : base (height, width, x, y, direction)
        {
            Speed = 2;
            Source = MediaHelper.GetBitmapImage("Fireball\\0");
            _animationMax = 1;
            Height = Height;
            Width = Height / 2;
        }

        public override void Animate(object sender, EventArgs e)
        {
            base.Animate(sender, e);

            Source = MediaHelper.GetBitmapImage("Fireball\\" + _animationIndex);
        }

        public override void UpdateProjectileRect()
        {
            HitBox = new RectangleGeometry(GetHitBoxRect(5));
        }
    }
}
