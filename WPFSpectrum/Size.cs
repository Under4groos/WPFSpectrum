using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSpectrum
{
    public class Size
    {
        public double X
        {
            get;set;
        }
        public double Y
        {
            get; set;
        }
        public Size(double x , double y)
        {
            X = x; Y = y;
        }
        public Size()
        {
            X = 0; Y = 0;
        }
        public Size(double xy)
        {
            Y = X = xy;
        }
        //public void SetSize(ref double w , ref double h, Size s)
        //{
        //    w = s.X;
        //    h = s.Y;
        //}
        public static Size operator +(Size c1, Size c2)
            => new Size(c1.X + c2.X, c1.Y + c2.Y);
        public static Size operator -(Size c1, Size c2)
            => new Size(c1.X - c2.X, c1.Y - c2.Y);
        public static Size operator *(Size c1, double d)
            => new Size(c1.X * d, c1.Y * d);

        public override string ToString()
        {
            return $"{X}*{Y}";
        }
    }
}
