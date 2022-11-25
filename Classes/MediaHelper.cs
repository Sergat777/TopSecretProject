using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GreatApparatusYebat.Classes
{
    public static class MediaHelper
    {
        public static string AppMainDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static string ImagesDirectory = AppMainDirectory + "\\Pictures\\";
        public static string SoundsDirectory = AppMainDirectory + "\\Sounds\\";

        // Get bitmap from images directory
        public static BitmapImage GetBitmapImage(string imageName)
        {
            return new BitmapImage(new Uri(ImagesDirectory + imageName + ".png"));
        }

        public static void PlayMusic(string musicName)
        {
            MediaElement media = new MediaElement();
            media.Source = new Uri(SoundsDirectory + musicName + ".wav");
            media.MediaEnded += RepeatMedia;
            AppControls.AppMainPanel.Children.Add(media);
        }

        public static void PlaySound(string soundName)
        {
            MediaElement media = new MediaElement();
            media.Source = new Uri(SoundsDirectory + soundName + ".wav");
            AppControls.AppMainPanel.Children.Add(media);
        }

        private static void RepeatMedia(object sender, EventArgs e)
        {
            (sender as MediaElement).Position = TimeSpan.Zero;
        }
    }
}
