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
    /// Логика взаимодействия для Button.xaml
    /// </summary>
    public partial class Button : UserControl
    {
        public Button()
        {
            InitializeComponent();
            SizeFont = 15;
            BackgroundHoverLeave = Color.FromArgb(100, 75, 75, 75);
            BackgroundHoverEnter = Color.FromArgb(100, 85, 85, 85);
        }

        public double CornerRadius
        {
            get
            {
                return (double)GetValue(CornerRadius_);
            }
            set
            {
                SetValue(CornerRadius_, value);
            }
        }


        public static readonly DependencyProperty CornerRadius_ = DependencyProperty.Register(
            "CornerRadius", typeof(double), typeof(Button),
            new PropertyMetadata(
                0.0,
                (o, args) => ((Button)o).Update())
            );
        public double SizeFont
        {
            get
            {
                return (double)GetValue(SizeFont_);
            }
            set
            {
                SetValue(SizeFont_, value);
            }
        }
        public static readonly DependencyProperty SizeFont_ = DependencyProperty.Register(
            "SizeFont", typeof(double), typeof(Button),
            new PropertyMetadata(
                0.0,
                (o, args) => ((Button)o).Update())
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
            "Text", typeof(string), typeof(Button),
            new PropertyMetadata(
                "",
                (o, args) => ((Button)o).Update())
            );

        public Color BackgroundHoverLeave
        {
            get
            {
                return (Color)GetValue(BackgroundHoverLeave_);
            }
            set
            {
                SetValue(BackgroundHoverLeave_, value);
            }
        }
        public static readonly DependencyProperty BackgroundHoverLeave_ = DependencyProperty.Register(
            "BackgroundHoverLeave", typeof(Color), typeof(Button),
            new PropertyMetadata(
                Color.FromArgb(0,0,0,0),
                (o, args) => ((Button)o).Update())
            );
        public Color BackgroundHoverEnter
        {
            get
            {
                return (Color)GetValue(BackgroundHoverEnter_);
            }
            set
            {
                SetValue(BackgroundHoverEnter_, value);
            }
        }
        public static readonly DependencyProperty BackgroundHoverEnter_ = DependencyProperty.Register(
            "BackgroundHoverEnter", typeof(Color), typeof(Button),
            new PropertyMetadata(
                Color.FromArgb(0, 0, 0, 0),
                (o, args) => ((Button)o).Update())
            );
        void Update()
        {
            Border_button2.CornerRadius = Border_button.CornerRadius = new CornerRadius(CornerRadius);

            Label_text.Content = Text;
            Label_text.FontSize = SizeFont;
            Border_button2.Background = new SolidColorBrush(BackgroundHoverLeave);
            //Border_button1.Background = new SolidColorBrush(BackgroundHoverEnter);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Border_button2.Background = new SolidColorBrush(BackgroundHoverLeave);
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Border_button2.Background = new SolidColorBrush(BackgroundHoverEnter);
        }
    }
}
