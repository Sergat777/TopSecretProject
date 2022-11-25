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
using System.Windows.Threading;

namespace GreatApparatusYebat.ProjectalesClasses
{
    public enum Directions
    {
        LeftToRight,
        RightToLeft,
        TopToBottom,
        BottomToTop,
        Mixed
    }
    public class Projectile : Image
    {
        public Geometry HitBox { get; set; }
        public int Speed { get; set; } = 3;
        public int Damage { get; set; } = 2;
        public Directions Direction { get; set; }
        public bool ToRight { get; set; }
        public bool ToBottom { get; set; }
        public bool IsIntersecting { get; set; }
        protected byte _animationIndex = 0;
        protected byte _animationMax = 0;

        DispatcherTimer animationTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(500) };
        public Projectile(  int height = 30,
                            int width = 30,
                            int x = 0,
                            int y = 0,
                            Directions direction = Directions.Mixed,
                            bool toRight = true,
                            bool toBottom = true)
        {
            Height = height;
            Width = width;
            ToRight = toRight;
            ToBottom = toBottom;
            Direction = direction;
            Stretch = Stretch.Fill;
            animationTimer.Tick += Animate;
            animationTimer.Start();

            if (Direction == Directions.Mixed)
            {
                if (ToRight)
                {
                    Canvas.SetLeft(this, x - Width);
                    RenderTransform = new RotateTransform(90);
                }
                else
                {
                    Canvas.SetRight(this, x - Width);
                    RenderTransform = new RotateTransform(-90);
                }

                if (ToBottom)
                    Canvas.SetTop(this, y);
                else
                    Canvas.SetBottom(this, y);
            }

            if (Direction == Directions.TopToBottom)
            {
                RenderTransform = new RotateTransform(180);
                Canvas.SetLeft(this, x + Width);
                Canvas.SetTop(this, 0 - Height);
            }

            if (Direction == Directions.BottomToTop)
            {
                Canvas.SetLeft(this, x);
                Canvas.SetBottom(this, 0 - Height);
            }

            if (Direction == Directions.LeftToRight)
            {
                RenderTransform = new RotateTransform(90);
                Canvas.SetLeft(this, 0 - Height);
                Canvas.SetTop(this, y + Width);
            }

            if (Direction == Directions.RightToLeft)
            {
                RenderTransform = new RotateTransform(-90);
                Canvas.SetRight(this, 0 - Height);
                Canvas.SetTop(this, y + Width);
            }
        }

        public virtual void Animate(object sender, EventArgs e)
        {
            if (_animationIndex != _animationMax)
                _animationIndex++;
            else
                _animationIndex = 0;
        }

        public virtual void Move()
        {
            if (Direction == Directions.Mixed)
            {
                if (ToRight)
                    Canvas.SetLeft(this, Canvas.GetLeft(this) + Speed);
                else
                    Canvas.SetRight(this, Canvas.GetRight(this) + Speed);

                if (ToBottom)
                    Canvas.SetTop(this, Canvas.GetTop(this) + Speed);
                else
                    Canvas.SetBottom(this, Canvas.GetBottom(this) + Speed);
            }
            else
                if (Direction == Directions.LeftToRight)
                Canvas.SetLeft(this, Canvas.GetLeft(this) + Speed);
            else
                if (Direction == Directions.RightToLeft)
                Canvas.SetRight(this, Canvas.GetRight(this) + Speed);
            else
                if (Direction == Directions.TopToBottom)
                Canvas.SetTop(this, Canvas.GetTop(this) + Speed);
            else
                Canvas.SetBottom(this, Canvas.GetBottom(this) + Speed);

            UpdateProjectileRect();
            CheckIntersectWithHero();
        }

        public virtual void UpdateProjectileRect()
        {
            HitBox = new RectangleGeometry(GetHitBoxRect(0));
        }

        // DO NOT TOUCH BLYAT!
        public Rect GetHitBoxRect(int smallIndex)
        {
            double x = 0;
            double y = 0;
            Rect rect = new Rect(x, y, Width, Height);

            if (Direction == Directions.Mixed)
            {
                rect = new Rect();
                if (ToRight)
                {
                    rect.X = Canvas.GetLeft(this) - Width;
                }
                else
                    rect.X = AppControls.MainCanvas.ActualWidth - Canvas.GetRight(this) - Width;

                if (ToBottom)
                {
                    if (!ToRight)
                        rect.Y = Canvas.GetTop(this) - Height;
                    else
                        rect.Y = Canvas.GetTop(this);
                }
                else
                {
                    if (!ToRight)
                        rect.Y = AppControls.MainCanvas.ActualHeight - Canvas.GetBottom(this) - Height * 2;
                    else
                        rect.Y = AppControls.MainCanvas.ActualHeight - Canvas.GetBottom(this) - Height;
                }

                rect.Width = Width;
                rect.Height = Height;
            }

            if (Direction == Directions.TopToBottom)
            {
                rect = new Rect(-Canvas.GetLeft(this), -Canvas.GetTop(this), Width, Height);
                rect.Transform(new RotateTransform(180).Value);
            }

            if (Direction == Directions.BottomToTop)
            {
                rect = new Rect(Canvas.GetLeft(this), AppControls.MainCanvas.ActualHeight - Canvas.GetBottom(this) - Height,
                    Width, Height);
            }

            if (Direction == Directions.LeftToRight)
            {
                rect = new Rect(Canvas.GetLeft(this) - Height, Canvas.GetTop(this), Height, Width);
            }

            if (Direction == Directions.RightToLeft)
            {
                rect = new Rect(AppControls.MainCanvas.ActualWidth - Canvas.GetRight(this) - Width,
                    Canvas.GetTop(this) - Width, Height, Width);
            }

            return new Rect(rect.X + smallIndex, rect.Y + smallIndex, rect.Width - smallIndex, rect.Height - smallIndex);
            //HitBox = new RectangleGeometry(rect);
        }

        public virtual bool CheckIntersectWithHero()
        {
            Geometry heroHitBox = AppControls.Player.GetHitBox();

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
