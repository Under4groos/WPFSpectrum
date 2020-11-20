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
    /// Логика взаимодействия для CheckBox.xaml
    /// </summary>
    public partial class CheckBox : UserControl
    {
        public CheckBox()
        {
            InitializeComponent();
        }

        public bool Active
        {
            get
            {
                return (bool)GetValue(Active_);
            }
            set
            {
                SetValue(Active_, value);
            }
        }
        public static readonly DependencyProperty Active_ = DependencyProperty.Register(
           "Active", typeof(bool), typeof(CheckBox),
           new PropertyMetadata(
               true,
               (o, args) => ((CheckBox)o).Update())
           );

        public string Text
        {
            get
            {
                return (string)GetValue(Text_);
            }
            set
            {
                SetValue(Text_, value);
            }
        }
        public static readonly DependencyProperty Text_ = DependencyProperty.Register(
           "Text", typeof(string), typeof(CheckBox),
           new PropertyMetadata(
               "",
               (o, args) => ((CheckBox)o).Update())
           );

       
        void Update()
        {
            switch (Active)
            {
                case true:
                    BorderColor.Background = new SolidColorBrush(Color.FromArgb(255,127, 255, 0));
                    break;
                case false:
                    BorderColor.Background = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    break;
                default:
                    break;
            }

            TextLabel.Content = Text;

        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Active = !Active;
        }
    }
}
