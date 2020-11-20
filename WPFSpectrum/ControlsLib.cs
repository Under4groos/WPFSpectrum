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


        readonly static List<WPFControls.WPFLine> Lines = new List<WPFControls.WPFLine>();
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
            GR = grid;
            int count = (int)SizeWindowWidth / SizePanelWidth;
            double size_ = SizeWindowWidth / count;
            Debug.WriteLine($"Size:{size_} Count:{count}");
            for (int i = 0; i < count; i++)
            {
                WPFControls.WPFLine wPFLine = new WPFControls.WPFLine
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
    }
}
