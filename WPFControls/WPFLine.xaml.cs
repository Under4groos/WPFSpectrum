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

namespace WPFControls
{
    /// <summary>
    /// Логика взаимодействия для WPFLine.xaml
    /// </summary>
    public partial class WPFLine : UserControl
    {
        public WPFLine()
        {
            InitializeComponent();
        }
        public double SizeHeight
        {
            get { return (double)GetValue(SizeHeight_); }
            set { SetValue(SizeHeight_, value); }
        }
        public static readonly DependencyProperty SizeHeight_ = DependencyProperty.Register(
            "SizeHeight", typeof(double), typeof(WPFLine),
            new PropertyMetadata(
                0.0,
                (o, args) => ((WPFLine)o).Update())
            );
        void Update()
        {
            WPFBorder.Height = SizeHeight < 5 ? 5 : SizeHeight;
        }
    }
}
