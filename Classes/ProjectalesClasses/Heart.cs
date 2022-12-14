using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace GreatApparatusYebat.Classes.ProjectalesClasses
{
    public class Heart : Image
    {
        public int Speed = 5;
        public byte Health { get; set; } = 20;
        public bool IsProtect { get; set; } = false;

        private byte _protectionIndex = 0;
        DispatcherTimer protectionTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(200) };
        
        public Heart()
        {
            Height = 30;
            Width = 30;
            Source = MediaHelper.GetBitmapImage("heart");

            protectionTimer.Tick += ProtectHeart;
        }

        public void ApplyDamage(int damage)
        {
            if (damage >= Health)
                Health = 0;
            else
                Health -= (byte)damage;

            IsProtect = true;
            protectionTimer.Start();

            AppControls.HealthBar.Value = Health;
            MediaHelper.PlaySound("damageSound");
        }

        public void ProtectHeart(object sender, EventArgs e)
        {
            _protectionIndex++;

            if (_protectionIndex % 2 != 0)
                Opacity = 0.45;
            else
                Opacity = 1;

            if (_protectionIndex == 8)
            {
                IsProtect = false;
                _protectionIndex = 0;
                protectionTimer.Stop();
            }
        }

        public void MoveToLeft()
        {
            if (Canvas.GetLeft(this) - Speed > 0)
                Canvas.SetLeft(this, Canvas.GetLeft(this) - Speed);
            else
                Canvas.SetLeft(this, 0);
        }

        public void MoveToRight()
        {
            if (Canvas.GetLeft(this) + Speed < AppControls.MainCanvas.ActualWidth - Width)
                Canvas.SetLeft(this, Canvas.GetLeft(this) + Speed);
            else
                Canvas.SetLeft(this, AppControls.MainCanvas.ActualWidth - Width);
        }

        public void MoveToUp()
        {
            if (Canvas.GetTop(this) - Speed > 0)
                Canvas.SetTop(this, Canvas.GetTop(this) - Speed);
            else
                Canvas.SetTop(this, 0);
        }

        public void MoveToDown()
        {
            if (Canvas.GetTop(this) + Speed < AppControls.MainCanvas.ActualHeight - Height)
                Canvas.SetTop(this, Canvas.GetTop(this) + Speed);
            else
                Canvas.SetTop(this, AppControls.MainCanvas.ActualHeight - Height);
        }

        public Geometry GetHitBox()
        {
            return new EllipseGeometry(new System.Windows.Rect()
            {
                Height = Height - 5,
                Width = Width - 5,
                X = Canvas.GetLeft(this) + 5,
                Y = Canvas.GetTop(this) + 5
            });
        }
    }
}
