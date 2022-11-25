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
    public enum StraightDirections
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
        public StraightDirections Direction { get; set; }
        public bool ToRight { get; set; }
        public bool ToBottom { get; set; }
        public bool IsIntersecting { get; set; }
        private byte _animationIndex = 0;

        DispatcherTimer animationTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(200) };
        public Projectile(  int height = 30,
                            int width = 30,
                            int x = 0,
                            int y = 0,
                            StraightDirections direction = StraightDirections.Mixed,
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

            if (ToRight)
            {
                Canvas.SetLeft(this, x - Width);
                FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                Canvas.SetRight(this, x - Width);
                FlowDirection = FlowDirection.RightToLeft;
            }

            if (ToBottom)
                Canvas.SetTop(this, y);
            else
                Canvas.SetBottom(this, y);

            if (Direction == StraightDirections.TopToBottom)
            {
                RenderTransform = new RotateTransform(180);
                //FlowDirection = FlowDirection.RightToLeft;
                Canvas.SetLeft(this, x + Width);
                Canvas.SetTop(this, 0);
            }

            if (Direction == StraightDirections.BottomToTop)
            {
                //RenderTransform = new RotateTransform(180);
                //FlowDirection = FlowDirection.RightToLeft;
                Canvas.SetLeft(this, x);
                Canvas.SetBottom(this, 0 - Height);
            }

            if (Direction == StraightDirections.LeftToRight)
            {
                RenderTransform = new RotateTransform(90);
                FlowDirection = FlowDirection.LeftToRight;
                Canvas.SetLeft(this, 0);
                Canvas.SetTop(this, y);
            }

            if (Direction == StraightDirections.RightToLeft)
            {
                RenderTransform = new RotateTransform(-90);
                Canvas.SetRight(this, 0 - Width);
                Canvas.SetTop(this, y);
            }
        }

        public void Animate(object sender, EventArgs e)
        {
            try
            {
                _animationIndex++;
                Source = MediaHelper.GetBitmapImage("Arrow\\" + _animationIndex);
            }
            catch
            {
                _animationIndex = 0;
                Source = MediaHelper.GetBitmapImage("Arrow\\" + _animationIndex);
            }
        }

        public virtual void Move()
        {
            if (Direction == StraightDirections.Mixed)
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
                if (Direction == StraightDirections.LeftToRight)
                Canvas.SetLeft(this, Canvas.GetLeft(this) + Speed);
            else
                if (Direction == StraightDirections.RightToLeft)
                Canvas.SetRight(this, Canvas.GetRight(this) + Speed);
            else
                if (Direction == StraightDirections.TopToBottom)
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

        public Rect GetHitBoxRect(int smallIndex)
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

            Rect rect = new Rect(x, y, Width, Height);

            if (Direction == StraightDirections.TopToBottom)
            {
                rect = new Rect(-Canvas.GetLeft(this), -Canvas.GetTop(this), Width, Height);
                rect.Transform(new RotateTransform(180).Value);
            }

            if (Direction == StraightDirections.BottomToTop)
            {
                rect = new Rect(x, AppControls.MainCanvas.ActualHeight - Canvas.GetBottom(this) - Height,
                    Width, Height);
            }

            if (Direction == StraightDirections.LeftToRight)
            {
                rect = new Rect(x - Width, Canvas.GetTop(this), Width, Height);
            }

            if (Direction == StraightDirections.RightToLeft)
            {
                rect = new Rect(x = AppControls.MainCanvas.ActualWidth - Canvas.GetRight(this) - Width,
                    Canvas.GetTop(this) - Height, Width, Height);
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
