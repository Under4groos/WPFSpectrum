using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFControls;

namespace WPFSpectrum
{
    static class ControlsLib
    {

        public static void ControlWindNotifysetColor(Border ui , byte a , byte r , byte g , byte b)
        {         
                ui.BorderBrush = new System.Windows.Media.SolidColorBrush(Color.FromArgb(a, r, g, b));
        }
        public static void IntDec(double num , double min)
        {

            for (int i = 0; i < Count(); i++)
            {
                GetElementByID(i).SizeHeight -= num>min?num:min;
            }

        }

        public readonly static List<WPFControls.WPFLine> Lines = new List<WPFControls.WPFLine>();
        static Grid GR;
        /// <summary>
        /// Получить из листа элемент по его ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static WPFControls.WPFLine GetElementByID(int id)
        {
            if (id > Lines.Count)
                id = Lines.Count;
            if (id < 0)
                id = 0;
            return Lines[id];
        }
        /// <summary>
        /// Получить кол-во линий.
        /// </summary>
        /// <returns></returns>
        public static int Count()
        {
            return Lines.Count();
        }
        /// <summary>
        /// Очистка списка линий с Grid и листа.
        /// </summary>
        public static void Clear()
        {
            if (GR != null)
                GR.Children.Clear();
            Lines.Clear();
        }
        /// <summary>
        /// Создание линий подстраивающихся под размер окна с учетов их размера.
        /// </summary>
        /// <param name="SizeWindowHeight"></param>
        /// <param name="SizeWindowWidth"></param>
        /// <param name="SizePanelWidth"></param>
        /// <param name="grid"></param>
        public static void CreateLine(
           double SizeWindowHeight, double SizeWindowWidth, int SizePanelWidth,Grid grid , Color color)
        {
            Clear();
            GR = grid;
            int sw = (int)SizeWindowWidth>0? (int)SizeWindowWidth:1;
            int sp = SizePanelWidth >0? SizePanelWidth:1;
            int count = sw / sp;           
            int size_ = (int)(SizeWindowWidth / count);
            Debug.WriteLine($"Size:{size_} Count:{count}");
            for (int i = 0; i < count; i++)
            {
                WPFLine wPFLine = new WPFLine
                {
                    Height = SizeWindowHeight,
                    Width = size_,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    SizeHeight = 5,
                    ColorLine = color
                    
                };              
                wPFLine.Margin = new Thickness(size_ * i, 0, 0,2);
                grid.Children.Add(wPFLine);
                Lines.Add(wPFLine);
            }
        }
        public static void Default(double Incr = 1.8)
        {
            for (int i = 0; i < Count(); i++)
            {
                
                GetElementByID(i).SizeHeight = 5;
            }
        }
        public static void SetActiveBorder(Border b, bool a)
        {
            b.BorderBrush = a ? new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)) :
                new SolidColorBrush(Color.FromArgb(0, 255, 0, 0));
        }
        public static void SmoothHistogram(List<WPFLine> originalValues)
        {
            List<double> smoothedValues = new List<double>();
            foreach (var item in originalValues)
            {
                smoothedValues.Add(item.SizeHeight);
            }
            
            double[] mask = new double[] { 0.25, 0.5, 0.25 };
            for (int bin = 1; bin < originalValues.Count - 2; bin++)
            {
                double smoothedValue = 0;
                for (int i = 0; i < mask.Length; i++)
                {
                    smoothedValue += originalValues[bin - 1 + i].SizeHeight * mask[i];
                }
                smoothedValues[bin] = (int)smoothedValue;
            }
            for (int i = 0; i < originalValues.Count; i++)
            {
                originalValues[i].SizeHeight = smoothedValues[i];
            }
        }

    }
}
