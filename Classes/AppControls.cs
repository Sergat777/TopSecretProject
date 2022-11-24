using GreatApparatusYebat.Classes.ProjectalesClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace GreatApparatusYebat.Classes
{
    public static class AppControls
    {
        public static DockPanel AppMainPanel { get; set; }
        public static Heart Player { get; set; }
        public static Canvas MainCanvas { get; set; }
        public static ProgressBar HealthBar { get; set; }
    }
}
