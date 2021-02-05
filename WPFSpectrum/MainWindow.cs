using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using WPFControls;
using WPFNaudioLib;
using WpfSpectrum;
using WPFSpectrum.Lib;

namespace WPFSpectrum
{
    public partial class MainWindow : Window
    {
        #region new Class: Audio TimerTick NotifyIcon WindNotify Configuration Json
        readonly Audio audio = new Audio();
        readonly TimerTick timer = new TimerTick();
        readonly NotifyIcon ntic = new NotifyIcon();
        WindNotify windNotify = new WindNotify();      
        Configuration cfg = new Configuration();
        readonly Json j = new Json();
        #endregion  
   

        public MainWindow()
        {
            InitializeComponent();

            System.Windows.MessageBox.Show($"{new Win_Screen(this).ScreenSize}");

            #region OpenNewSetttingProgram
            if (j.IsVoidFile())
            {

                j.OpenJson();
                if (j.isOpen)
                {
                    cfg = j.Config;
                }
                else
                {
                    cfg = new Configuration();
                }
                    

            }
            else
            {

                j.Config = cfg;
                j.SaveJson();
            }

            Setting();
            #endregion

            

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
                        windNotify.Topmost = true;
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

        public void Setting()
        {
            SetSize(cfg.SizeWindow);
            SetPos(cfg.PositionWindow);

            windNotify.textBoxes[0].Text = cfg.SizeWindow.ToString();
            windNotify.textBoxes[1].Text = cfg.CountSmoothHistogram.ToString();
            windNotify.textBoxes[2].Text = cfg.SizeLineHeight.ToString();
            windNotify.textBoxes[3].Text = cfg.PositionWindow.ToString();
            windNotify.textBoxes[4].Text = cfg.SmoothnessLine.ToString();

            windNotify.CheckBoxs[0].Active = cfg.TomMost;
            windNotify.CheckBoxs[1].Active = cfg.isSmoothness;
            
            Dispatcher.BeginInvoke(
               new ThreadStart(delegate {
                   ControlsLib.CreateLine(this.Height, this.Width, cfg.SizeLineHeight, ListLine, cfg.ColorLine);
               }));
            timer.Time = cfg.TimeInterval;

            audio.Dada = cfg.Dada;
            audio.Db = cfg.Db;

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
            //Debug.WriteLine(cfg.isSmoothness);
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
            #region SmoothnessLine
            if (double.TryParse(windNotify.textBoxes[4].Text, out double s_))
            {
                cfg.SmoothnessLine = s_;
            }
            #endregion

            cfg.ColorLine = windNotify.ColorBoxs[0].ColorARGB;
            SaveSetting();
            Setting();
        }

        public void SaveSetting()
        {
            j.Config = cfg;
            j.SaveJson();

        }
 
        void Timer_Tick(object sender ,EventArgs e)
        {
            this.Topmost = cfg.TomMost;
            switch (AudioDevice.IsWorking())
            {              
                case true:
                    if (j.isUpdateFile())
                    {
                        if (windNotify.IsActive)
                            return;
                        //Debug.WriteLine("Обновление настроек...");
                        Thread.Sleep(100);
                        j.OpenJson();
                        cfg = j.Config;
                        Setting();

                    }
                    if (audio.GetStatus == false)
                    {
                        ControlsLib.IsMinAll = true;
                    }
                    
                    audio.StartRecording();
                    double inc = ControlsLib.MinAllDouble();

                    for (int i = 0; i < ControlsLib.Count(); i++)
                    {
                        double size_h = audio.list_array[i];
                        double last_size_h = ControlsLib.GetElementByID(i).SizeHeight;
                        double len_double = last_size_h;

                        size_h = size_h > 5 ? this.Height - size_h  : 5;

                        
                        if (size_h > last_size_h)
                        {
                            switch (cfg.isSmoothness)
                            {
                                case true:
                                    len_double += size_h * cfg.SmoothnessLine;
                                    break;
                                case false:
                                    len_double = size_h;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            len_double -= cfg.Increment;
                        }
                        ControlsLib.GetElementByID(i).SizeHeight = len_double - inc * 0.98;
                    }

                    ControlsLib.SmoothHistogram(ControlsLib.Lines);

                    break;
                case false:
                    #region Не трогай блять Оно и так зАибись робит!
                    int Csi = ControlsLib.Count() * 5;
                    int si = 0;
                    if (ControlsLib.IsMinAll)
                    {
                        

                        for (int i = 0; i < ControlsLib.Count(); i++)
                        {

                            double d = ControlsLib.GetElementByID(i).SizeHeight -= cfg.Increment;

                            si += d < 5? 5 : (int)d;
                        }
                        if (Csi == si)
                            ControlsLib.IsMinAll = false;   
                    }
                    else 
                    {
                        audio.StopRecording();
                    }
                    #endregion
                    break;
                default:
                    break;
            }
            

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
            Process.GetCurrentProcess().Kill();          
        }
    }
}
