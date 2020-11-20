using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSpectrum
{
    class Point
    {
        public double X
        {
            get; set;
        }
        public double Y
        {
            get; set;
        }
        public Point(double x, double y)
        {
            X = x; Y = y;
        }
        public Point()
        {
            X = 0; Y = 0;
        }
        public Point(double xy)
        {
            Y = X = xy;
        }
        public static Point operator +(Point c1, Point c2)
            => new Point(c1.X + c2.X, c1.Y + c2.Y);
        public static Point operator -(Point c1, Point c2)
            => new Point(c1.X - c2.X, c1.Y - c2.Y);
        public static Point operator *(Point c1, double d)
            => new Point(c1.X * d, c1.Y * d);
    }
}
