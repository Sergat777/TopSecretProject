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
                        StraightDirections direction = StraightDirections.Mixed) : base (height, width, x, y, direction)
        {
            Speed = 5;
            Source = MediaHelper.GetBitmapImage("Fireball\\0");
            Height = Height;
            Width = Height / 2;
        }
    }
}
