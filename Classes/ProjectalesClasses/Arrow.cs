using GreatApparatusYebat.ProjectalesClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GreatApparatusYebat.Classes.ProjectalesClasses
{
    public class Arrow : Projectile
    {
        public Arrow(int height = 20,
                     int width = 20,
                     int x = 0,
                     int y = 0,
                     Directions direction = Directions.Mixed,
                     bool toRight = true,
                     bool toBottom = true) : base(height, width, x, y, direction, toRight, toBottom)
        {
            Source = MediaHelper.GetBitmapImage("Arrow\\0");
            _animationMax = 3;
        }

        public override void Animate(object sender, EventArgs e)
        {
            base.Animate(sender, e);

            Source = MediaHelper.GetBitmapImage("Arrow\\" + _animationIndex);
        }

        public override void UpdateProjectileRect()
        {
            HitBox = new RectangleGeometry(GetHitBoxRect(3));
        }
    }
}
