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

namespace WPFSpectrum
{
    public partial class MainWindow : Window
    {
        readonly Audio audio = new Audio();
        readonly TimerTick timer = new TimerTick();
        readonly NotifyIcon ntic = new NotifyIcon();
        WindNotify windNotify = new WindNotify();
        private System.Drawing.Rectangle ScreenSize = Screen.PrimaryScreen.WorkingArea;
        readonly Configuration cfg = new Configuration();
        readonly Json j = new Json();

        public MainWindow()
        {
            InitializeComponent();

            if (j.IsVoidFile())
            {
                try
                {
                    j.OpenJson();
                    cfg = (Configuration)j.Config;
                }
                catch (Exception){}
            }
            else
            {

                j.Config = cfg;
                j.SaveJson();
            }



            this.Top = ScreenSize.Height - this.Height;
            this.Left = 0;



            this.Width = cfg.SizeWindow.X;
            this.Height = cfg.SizeWindow.Y;
            windNotify.textBoxes[0].Text = string.Format("{0}*{1}", cfg.SizeWindow.X, cfg.SizeWindow.Y) ;
            windNotify.CheckBoxs[0].Active = cfg.TomMost;
            windNotify.textBoxes[2].Text = cfg.SizeLineHeight.ToString();

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
                        windNotify.EventMouseDownChanged(windNotify.CheckBoxs[0], EventClick);

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

        private void EventClick(object sender, MouseButtonEventArgs e)
        {
            cfg.TomMost = windNotify.CheckBoxs[0].Active;
            SaveSetting();
        }

        void EventText_notify(object sender, TextChangedEventArgs e)
        {
            string text_Smooth = windNotify.textBoxes[1].Text;
            if (int.TryParse(text_Smooth, out int c))
            {
                audio.CSmoothHistogram = cfg.CountSmoothHistogram = c;               
            }
            string text_size = windNotify.textBoxes[0].Text;

            string[] strarray = text_size.Split(new char[] { '*' });
            if (strarray.Length != 2)
                return;
            if (int.TryParse(strarray[0], out int x) && int.TryParse(strarray[1], out int y))
            {
                cfg.SizeWindow = new Size(x, y);
                this.Width = x;
                this.Height = y;

                this.Top = ScreenSize.Height - this.Height;
                this.Left = 0;
            }
            string text_sizeline = windNotify.textBoxes[2].Text;
            if(int.TryParse(text_sizeline, out int s))
            {
                cfg.SizeLineHeight = s > 2? s:2;
                ControlsLib.Clear();
                ControlsLib.CreateLine(this.Height, this.Width, cfg.SizeLineHeight, ListLine, cfg.ColorLine);
            }


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
            int Count_control = ControlsLib.Count();
            int Count_fft_array = audio.list_array.Count;
            if (Count_control > Count_fft_array)
            {
                count = Count_fft_array;
            }else if(Count_control < Count_fft_array)
            {
                count = Count_control;
            }
                
            


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
                    ControlsLib.GetElementByID(i).SizeHeight = size_h;
                    
                }
                else
                {
                     ControlsLib.GetElementByID(i).SizeHeight -= last_size_h * cfg.Increment * 0.1;
                    // ControlsLib.GetElementByID(i).ColorLine = cfg.ColorLine;
                }
                ControlsLib.GetElementByID(i).ColorLine = cfg.ColorLine;
                
            }

            
            this.Topmost = cfg.TomMost;
            //if (windNotify.IsActive == true)
            //{
            //    ControlsLib.ControlWindNotifysetColor(BorderIsActiveSettingWind, 255, 255, 0, 0);
            //}
            //else
            //{
            //    ControlsLib.ControlWindNotifysetColor(BorderIsActiveSettingWind, 0, 0, 0, 0);
            //}

        }
        private void ListLabel_Loaded(object sender, RoutedEventArgs e)
        {
            ControlsLib.CreateLine(this.Height,this.Width, cfg.SizeLineHeight, ListLine, cfg.ColorLine);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ControlsLib.Clear();
            ControlsLib.CreateLine(this.Height, this.Width, cfg.SizeLineHeight, ListLine , cfg.ColorLine);

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
            audio.StopRecording();
            timer.Stop();
            
        }
    }
}
