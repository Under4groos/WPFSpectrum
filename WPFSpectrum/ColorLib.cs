using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPFSpectrum
{
    static class ColorLib
    {
        public static Color ColorDev( Color c1 , Color c2 )
        {
            return Color.FromArgb(
                (byte)((c1.A + c2.A) / 2) ,
                (byte)((c1.R + c2.R) / 2),
                (byte)((c1.G + c2.G) / 2),
                (byte)((c1.B + c2.B) / 2));
        }
    }
}
