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
        public Fireball(int height = 100,
                        int width = 100,
                        int x = 0,
                        int y = 0,
                        Directions direction = Directions.Mixed) : base (height, width, x, y, direction)
        {
            Speed = 2;
            Direction = direction;
            Source = MediaHelper.GetBitmapImage("Fireball\\0");
            _animationMax = 7;
            Height = height;
            Width = height / 2;
            _animationTimer.Interval = TimeSpan.FromMilliseconds(150);
            SetStartPositions(x, y);
        }

        public override void SetNextSprite(object sender, EventArgs e)
        {
            base.SetNextSprite(sender, e);

            Source = MediaHelper.GetBitmapImage("Fireball\\" + _animationIndex);
        }

        public override void UpdateProjectileRect()
        {
            HitBox = new RectangleGeometry(GetHitBoxRect(7));
        }
    }
}
