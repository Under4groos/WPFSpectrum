using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFNaudioLib;
using WPFControls;
using WpfSpectrum;
using System.Diagnostics;

namespace WPFSpectrum
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly Audio audio;
        readonly TimerTick timer = new TimerTick();
        public MainWindow()
        {
            InitializeComponent();

            audio = new Audio();
            

            timer.Time = 1;
            timer.Tick += Timer_Tick;
            timer.Start();
            audio.StartRecording();
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
                    ControlsLib.GetElementByID(i).SizeHeight -= last_size_h * 0.1;
                }


                

            }
        }
        private void ListLabel_Loaded(object sender, RoutedEventArgs e)
        {
            ControlsLib.CreateLine(this.Height,this.Width, 8, ListLabel);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ControlsLib.Clear();
            ControlsLib.CreateLine(this.Height, this.Width, 8, ListLabel);

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
            audio.StopRecording();
            timer.Stop();
            
        }
    }
}
