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
namespace WPFSpectrum
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Audio audio;
        public MainWindow()
        {
            InitializeComponent();

            audio = new Audio();
            audio.StartRecording();
            
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
    }
}
