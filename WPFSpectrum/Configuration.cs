using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WPFSpectrum;
namespace WPFSpectrum
{
    class Configuration
    {
        /// <summary>
        /// Сглаживание гистограмы в N кол-во раз.
        /// </summary>
        public int CountSmoothHistogram = 1;
        /// <summary>
        /// Размер линий.
        /// </summary>
        public int SizeLineHeight = 9;
        /// <summary>
        /// Плавность подьема линий
        /// </summary>
        public bool isSmoothness = true;
        /// <summary>
        /// Скорость плавного подьема 
        /// </summary>
        public double SmoothnessLine = 0.05;
        /// <summary>
        /// Интервал таймера.
        /// </summary>
        public int TimeInterval = 1;
        /// <summary>
        /// Плавное вычитание размера линий.
        /// </summary>
        public double Increment = 0.62;
        /// <summary>
        /// Размер окна с линиями.
        /// </summary>
        public Size SizeWindow = new Size(1200, 600);
        /// <summary>
        /// Позиция окна с линиями.
        /// </summary>
        public Point PositionWindow = new Point(1920, 1080);
        /// <summary>
        /// Цвет линий.
        /// </summary>
        public Color ColorLine = Color.FromArgb(255, 44, 255, 71);
        /// <summary>
        /// Промежуточный цвет A.
        /// </summary>
        public Color ColorLineA = Color.FromArgb(255, 255, 0, 0);
        /// <summary>
        /// Промежуточный цвет B.
        /// </summary>
        public Color ColorLineB = Color.FromArgb(255, 33, 255, 0);

        public bool TomMost = true;

    }
}
