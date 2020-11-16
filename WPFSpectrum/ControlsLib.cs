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

        public static WPFControls.WPFLine GetElementByID(int id)
        {           
            return Lines[id];
        }

        public static void CreateLine(
           double SizeWindowHeight, double SizeWindowWidth, int SizePanelWidth,Grid grid)
        {
            int count = (int)SizeWindowWidth / SizePanelWidth;
            double size_ = SizeWindowWidth / count;
            Debug.WriteLine(size_);
            for (int i = 0; i < count; i++)
            {
                WPFControls.WPFLine wPFLine = new WPFControls.WPFLine
                {
                    Height = SizeWindowHeight,
                    Width = size_,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    SizeHeight = 44
                    
                };
                
                wPFLine.Margin = new Thickness(size_ * i, 0, 0, 0);
                grid.Children.Add(wPFLine);
                Lines.Add(wPFLine);
            }
        }
    }
}
