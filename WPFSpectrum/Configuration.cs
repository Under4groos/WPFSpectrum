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
        public double SmoothnessLine = 0.09;
        /// <summary>
        /// Интервал таймера.
        /// </summary>
        public int TimeInterval = 1;
        /// <summary>
        /// Плавное вычитание размера линий.
        /// </summary>
        public double Increment = 2.42;
        /// <summary>
        /// Размер окна с линиями.
        /// </summary>
        public Size SizeWindow = new Size(800, 300);
        /// <summary>
        /// Позиция окна с линиями.
        /// </summary>
        public Point PositionWindow = new Point(10, 250);
        /// <summary>
        /// Цвет линий.
        /// </summary>
        public Color ColorLine = Color.FromArgb(255, 44, 255, 71);

        public double Db
        {
            get; set;
        } = -90;
        public double Dada
        {
            get; set;
        } = 12000;

        public bool TomMost = true;
        public override string ToString()
        {
            return $"";
        }
    }
}
