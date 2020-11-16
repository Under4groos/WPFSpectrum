using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
namespace WPFSpectrum
{
    static class ControlsLib
    {
        static List<WPFControls.WPFLine> Lines = new List<WPFControls.WPFLine>();
        static Grid GR;
        public static WPFControls.WPFLine GetElementByID(int id)
        {
            return id>Lines.Count? Lines[Lines.Count] : id<0? Lines[0] : Lines[id];
        }
        public static void Clear()
        {
            if (GR != null)
                GR.Children.Clear();
            Lines.Clear();
        }
        public static void CreateLine(
           double SizeWindowHeight, double SizeWindowWidth, int SizePanelWidth,Grid grid)
        {
            GR = grid;
            int count = (int)SizeWindowWidth / SizePanelWidth;
            double size_ = SizeWindowWidth / count;
            Debug.WriteLine($"Size:{size_} Count:{count}");
            for (int i = 1; i < count-3; i++)
            {
                WPFControls.WPFLine wPFLine = new WPFControls.WPFLine
                {
                    Height = SizeWindowHeight,
                    Width = size_,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    SizeHeight = 5

                };
                
                wPFLine.Margin = new Thickness(size_ * i, 0, 0,0);
                grid.Children.Add(wPFLine);
                Lines.Add(wPFLine);
            }
        }
    }
}
