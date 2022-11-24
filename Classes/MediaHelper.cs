using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        public static void PlaySound(string soundName)
        {
            MediaPlayer media = new MediaPlayer();
            media.Open(new Uri(SoundsDirectory + ".wav", UriKind.Relative));
            media.Volume = 0.7;
            media.Play();
        }
    }
}
