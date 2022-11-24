using GreatApparatusYebat.ProjectalesClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GreatApparatusYebat.Classes.ProjectalesClasses
{
    public class Arrow : Projectile
    {
        public Arrow(int height = 30,
                     int width = 30,
                     int x = 0,
                     int y = 0,
                     StraightDirections direction = StraightDirections.Mixed,
                     bool toRight = true,
                     bool toBottom = true) : base(height, width, x, y, direction, toRight, toBottom)
        {
            Source = MediaHelper.GetBitmapImage("Arrow\\0");

            //if (direction != StraightDirections.Mixed)
            //{
            //    //if (direction == StraightDirections.RightToLeft)
            //    //    FlowDirection = System.Windows.FlowDirection.RightToLeft;
            //    //if (direction == StraightDirections.TopToBottom)
            //    //    RenderTransform = new RotateTransform(90);
            //    //if (direction == StraightDirections.BottomToTop)
            //    //    RenderTransform = new RotateTransform(-90);
            //}

            
        }
    }
}
