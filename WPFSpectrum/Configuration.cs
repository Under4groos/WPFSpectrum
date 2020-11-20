using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSpectrum
{
     class Configuration
    {
        /// <summary>
        /// Сглаживание гистограмы в N кол-во раз.
        /// </summary>
        public  int CountSmoothHistogram = 1;
        /// <summary>
        /// Размер линий.
        /// </summary>
        [Range(3,99)]
        public  int SizeLineHeight = 9;
        /// <summary>
        /// Интервал таймера.
        /// </summary>
        public  int TimeInterval = 1;
        /// <summary>
        /// Плавное вычитание размера линий.
        /// </summary>
        public  double Increment = 0.62;
        /// <summary>
        /// Размер окна с линиями.
        /// </summary>
        public  Size SizeWindow = new Size(1200,600);
        /// <summary>
        /// Позиция окна с линиями.
        /// </summary>
        public  Size PositionWindow = new Size(800, 600);
    }
}
