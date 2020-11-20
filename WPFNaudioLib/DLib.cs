using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFNaudioLib
{
    public static class DLib
    {
        public static List<double> SmoothHistogram(List<double> originalValues)
        {
            List<double> smoothedValues = new List<double>();
            smoothedValues.AddRange(originalValues);
            double[] mask = new double[] { 0.25, 0.5, 0.25 };
            for (int bin = 1; bin < originalValues.Count - 2; bin++)
            {
                double smoothedValue = 0;
                for (int i = 0; i < mask.Length; i++)
                {
                    smoothedValue += originalValues[bin - 1 + i] * mask[i];
                }
                smoothedValues[bin] = (int)smoothedValue;
            }
            return smoothedValues;
        }
        public static double Lerp(double a, double b, double t)
        {
            return a + (b - a) * t;
        }
        public static double Map(double x, double x0, double x1, double a, double b)
        {
            return Lerp(a, b, (x - x0) / (x1 - x0));
        }
        public static double Deb(double a, double b)
        {
            return (double)(
                Math.Log(
                    Math.Sqrt(
                        Math.Pow(a, 2) + 
                        Math.Pow(b, 2)),                    
                    25
                    )
                );
        }


    }
}
