using GreatApparatusYebat.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GreatApparatusYebat.ProjectalesClasses
{
    public class Projectile : Image
    {
        public Geometry HitBox { get; set; }
        public int Speed { get; set; } = 3;
        public int Damage { get; set; } = 2;
        public bool ToRight { get; set; }
        public bool ToBottom { get; set; }
        public bool IsIntersecting { get; set; }

        public Projectile(  int height = 30,
                            int width = 30,
                            bool toRight = true,
                            bool toBottom = true,
                            int x = 0,
                            int y = 0)
        {
            Height = height;
            Width = width;
            ToRight = toRight;
            ToBottom = toBottom;

            if (ToRight)
            {
                Canvas.SetLeft(this, x);
                FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                Canvas.SetRight(this, x);
                FlowDirection = FlowDirection.RightToLeft;
            }

            if (ToBottom)
                Canvas.SetTop(this, y);
            else
                Canvas.SetBottom(this, y);
        }

        public virtual void Move()
        {
            if (ToRight)
                Canvas.SetLeft(this, Canvas.GetLeft(this) + Speed);
            else
                Canvas.SetRight(this, Canvas.GetRight(this) + Speed);

            if (ToBottom)
                Canvas.SetTop(this, Canvas.GetTop(this) + Speed);
            else
                Canvas.SetBottom(this, Canvas.GetBottom(this) + Speed);

            UpdateProjectileRect();
            CheckIntersectWithHero();
        }

        public virtual void UpdateProjectileRect()
        {
            double x;
            double y;

            if (ToRight)
                x = Canvas.GetLeft(this);
            else
                x = AppControls.MainCanvas.ActualWidth - Canvas.GetRight(this) - Width;

            if (ToBottom)
                y = Canvas.GetTop(this);
            else
                y = AppControls.MainCanvas.ActualHeight - Canvas.GetBottom(this) - Height;

            HitBox = new RectangleGeometry(new Rect(x, y, Width, Height));
        }

        public virtual bool CheckIntersectWithHero()
        {
            Geometry heroHitBox = new RectangleGeometry(new Rect(Canvas.GetLeft(AppControls.Player) + 7,
                                                      Canvas.GetTop(AppControls.Player) + 7,
                                                      AppControls.Player.Width - 7,
                                                      AppControls.Player.Height - 7));

            if (HitBox.FillContainsWithDetail(heroHitBox) == IntersectionDetail.Intersects &&
                !AppControls.Player.IsProtect)
            {
                AppControls.Player.ApplyDamage(Damage);
                IsIntersecting = true;
            }
            else
                if (HitBox.FillContainsWithDetail(heroHitBox) == IntersectionDetail.Empty)
                IsIntersecting = false;

            return HitBox.FillContainsWithDetail(heroHitBox) == IntersectionDetail.Intersects;
        }
    }
}
