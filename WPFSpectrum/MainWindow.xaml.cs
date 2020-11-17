using System;
using System.Windows;
using WPFNaudioLib;
using WpfSpectrum;
using System.Windows.Forms;
namespace WPFSpectrum
{
    public partial class MainWindow : Window
    {
        readonly Audio audio;
        readonly TimerTick timer = new TimerTick();
        readonly NotifyIcon ntic = new NotifyIcon();
        public MainWindow()
        {
            InitializeComponent();

            audio = new Audio();
            

            timer.Time = Configuration.TimeInterval;
            timer.Tick += Timer_Tick;
            timer.Start();

            audio.StartRecording();
            audio.CSmoothHistogram = Configuration.CountSmoothHistogram;

            ntic.Icon = Properties.Resources.Icon_ico;
            ntic.Visible = true;
            ntic.MouseClick += delegate (object sender, MouseEventArgs e)
            {

            };

        }


        void Timer_Tick(object sender ,EventArgs e)
        {

            for (int i = 0; i < ControlsLib.Count(); i++)
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
                        ControlsLib.GetElementByID(i).SizeHeight -= last_size_h * (Configuration.Increment * 0.25);
                    }

                



            }
            
        }
        private void ListLabel_Loaded(object sender, RoutedEventArgs e)
        {
            ControlsLib.CreateLine(this.Height,this.Width, Configuration.SizeLineHeight, ListLine);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ControlsLib.Clear();
            ControlsLib.CreateLine(this.Height, this.Width, Configuration.SizeLineHeight, ListLine);

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
            audio.StopRecording();
            timer.Stop();
            
        }
    }
}
