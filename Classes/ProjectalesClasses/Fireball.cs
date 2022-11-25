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
        public Fireball(int height = 20,
                        int width = 20,
                        int x = 0,
                        int y = 0,
                        Directions direction = Directions.Mixed) : base (height, width, x, y, direction)
        {
            Speed = 5;
            Source = MediaHelper.GetBitmapImage("Fireball\\0");
            _animationMax = 1;
            Height = Height;
            Width = Height / 2;
        }
    }
}
