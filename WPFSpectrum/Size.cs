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
    }
}
