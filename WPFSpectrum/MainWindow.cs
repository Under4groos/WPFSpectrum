using System;
using System.Windows;
using WPFNaudioLib;
using WpfSpectrum;
using WPFControls;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Windows.Media;
using System.Windows.Input;
using System.Threading;

namespace WPFSpectrum
{
    public partial class MainWindow : Window
    {
        readonly Audio audio = new Audio();
        readonly TimerTick timer = new TimerTick();
        readonly NotifyIcon ntic = new NotifyIcon();
        WindNotify windNotify = new WindNotify();      
        readonly Configuration cfg = new Configuration();
        readonly Json j = new Json();

    


        public MainWindow()
        {
            InitializeComponent();
           
            if (j.IsVoidFile())
            {

                j.OpenJson();
                cfg = j.Config;

            }
            else
            {

                j.Config = cfg;
                j.SaveJson();
            }



          



  
            SetSize(cfg.SizeWindow);
            SetPos(cfg.PositionWindow);

            windNotify.textBoxes[0].Text = cfg.SizeWindow.ToString();
            windNotify.textBoxes[1].Text = cfg.CountSmoothHistogram.ToString();
            windNotify.textBoxes[2].Text = cfg.SizeLineHeight.ToString();
            windNotify.textBoxes[3].Text = cfg.PositionWindow.ToString();
            windNotify.textBoxes[4].Text = cfg.SmoothnessLine.ToString();

            windNotify.CheckBoxs[0].Active = cfg.TomMost;
            windNotify.CheckBoxs[1].Active = cfg.isSmoothness;

            timer.Time = cfg.TimeInterval;
            timer.Tick += Timer_Tick;
            timer.Start();

            audio.StartRecording();
            audio.CSmoothHistogram = cfg.CountSmoothHistogram;

            ntic.Icon = Properties.Resources.Icon_ico;
            ntic.Visible = true;            
            ntic.MouseClick += delegate (object sender, System.Windows.Forms.MouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        break;
                    case MouseButtons.None:
                        break;
                    case MouseButtons.Right:
                        windNotify = windNotify ?? new WindNotify();
                        windNotify.SetPos();
                        windNotify.Show();
                        windNotify.Topmost = true;
                        windNotify.ColorBoxs[0].Text = $"{cfg.ColorLine.A},{cfg.ColorLine.R},{cfg.ColorLine.G},{cfg.ColorLine.B}";

                        foreach (var item in windNotify.textBoxes)
                        {
                            windNotify.EventTextChanged(item , EventText_notify);
                        }
                        foreach (var item in windNotify.CheckBoxs)
                        {
                            windNotify.EventMouseDownChanged(item, EventClick);
                        }
                        

                        foreach (var item in windNotify.ColorBoxs)
                        {
                            item.TextCh(EventText_notify);
                        }

                        break;
                    case MouseButtons.Middle:
                        break;
                    case MouseButtons.XButton1:
                        break;
                    case MouseButtons.XButton2:
                        break;
                    default:
                        break;
                }
            };

        }
        private void SetPos(Point p)
        {
           
            this.Left = p.X;
            this.Top  = p.Y;
        }
        private void SetSize(Size p)
        {
            this.Width = p.X;
            this.Height = p.Y;
        }
        private void EventClick(object sender, MouseButtonEventArgs e)
        {
            cfg.TomMost = windNotify.CheckBoxs[0].Active;
            cfg.isSmoothness = windNotify.CheckBoxs[1].Active;
            Debug.WriteLine(cfg.isSmoothness);
            SaveSetting();
        }

        void EventText_notify(object sender, TextChangedEventArgs e)
        {
            #region Size window
            string text_size = windNotify.textBoxes[0].Text;
            string[] strarray = text_size.Split(new char[] { '*' });
            if (strarray.Length != 2)
                return;
            if (int.TryParse(strarray[0], out int x) && int.TryParse(strarray[1], out int y))
            {
                x += 99;
                SetSize(cfg.SizeWindow = new Size(x, y));
                SetPos(cfg.PositionWindow);
            }
            #endregion
            #region Smooth
            string text_Smooth = windNotify.textBoxes[1].Text;
            if (int.TryParse(text_Smooth, out int c))
            {
                audio.CSmoothHistogram = cfg.CountSmoothHistogram = c;               
            }
            #endregion
            #region sizeline
            string text_sizeline = windNotify.textBoxes[2].Text;
            if(int.TryParse(text_sizeline, out int s))
            {
                cfg.SizeLineHeight = s > 3? s:3;
                ControlsLib.Clear();
                ControlsLib.CreateLine(this.Height, this.Width, cfg.SizeLineHeight, ListLine, cfg.ColorLine);
            }
            #endregion
            #region Position window
            string[] strarray_positionwind = windNotify.textBoxes[3].Text.Split(new char[] { '*' });
            if (strarray_positionwind.Length != 2)
                return;
            if (int.TryParse(strarray_positionwind[0], out int x_p) && int.TryParse(strarray_positionwind[1], out int y_p))
            {

                SetPos(cfg.PositionWindow =  new Point(
                    x_p, 
                    y_p
                    ));
                


            }
            #endregion

            if (double.TryParse(windNotify.textBoxes[4].Text, out double s_))
            {
                cfg.SmoothnessLine = s_;
            }


            // TextBoxSmoothing
            cfg.ColorLine = windNotify.ColorBoxs[0].ColorARGB;
            SaveSetting();
        }

        public void SaveSetting()
        {
            try
            {
                j.Config = cfg;
                j.SaveJson();
            }
            catch (Exception) { }
        }

        void Timer_Tick(object sender ,EventArgs e)
        {

            



            int count = 0;
            int Count_control = count = ControlsLib.Count();
            //int Count_fft_array = audio.list_array.Count;
            //if (Count_control > Count_fft_array)
            //{
            //    count = Count_fft_array;
            //}else if(Count_control < Count_fft_array)
            //{
            //    count = Count_control;
            //}
                
            


            for (int i = 0; i < count; i++)
            {
                double size_h = audio.list_array[i];
                double last_size_h = ControlsLib.GetElementByID(i).SizeHeight;
                
                if (size_h > 5)
                {
                    size_h = this.Height - size_h;
                }
                else
                {
                    size_h = 5;
                }
                if (size_h > last_size_h)
                {
                    switch (cfg.isSmoothness)
                    {
                        case true:
                            ControlsLib.GetElementByID(i).SizeHeight += size_h * cfg.SmoothnessLine;
                            break;
                        case false:
                            ControlsLib.GetElementByID(i).SizeHeight = size_h;
                            break;
                        default:
                            break;
                    }
                    

                }
                else
                {
                     ControlsLib.GetElementByID(i).SizeHeight -= last_size_h * cfg.Increment * 0.1;                  
                }
                ControlsLib.GetElementByID(i).ColorLine = cfg.ColorLine;                
            }
            ControlsLib.SmoothHistogram(ControlsLib.Lines);

            this.Topmost = cfg.TomMost;          
        }
        private void ListLabel_Loaded(object sender, RoutedEventArgs e)
            => Dispatcher.BeginInvoke(
                new ThreadStart(delegate {
                    ControlsLib.CreateLine(this.Height, this.Width, cfg.SizeLineHeight, ListLine, cfg.ColorLine);
                }));

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //ControlsLib.Clear();
            Dispatcher.BeginInvoke(
                new ThreadStart(delegate {
                    ControlsLib.CreateLine(this.Height, this.Width, cfg.SizeLineHeight, ListLine, cfg.ColorLine);
                }));
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
            //audio.StopRecording();
            //timer.Stop();
            Process.GetCurrentProcess().Kill();
            
        }
    }
}
