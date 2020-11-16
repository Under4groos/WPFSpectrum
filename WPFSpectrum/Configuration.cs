using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSpectrum
{
    static class Configuration
    {
        /// <summary>
        /// Сглаживание гистограмы в N кол-во раз.
        /// </summary>
        public static int CountSmoothHistogram = 2;
        /// <summary>
        /// Размер линий.
        /// </summary>
        public static int SizeLineHeight = 13;
        /// <summary>
        /// Интервал таймера.
        /// </summary>
        public static int TimeInterval = 1;
        /// <summary>
        /// Плавное вычитание размера линий.
        /// </summary>
        public static double Increment = 0.22;


    }
}
