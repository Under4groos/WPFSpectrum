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
    /// Логика взаимодействия для ColorBox.xaml
    /// </summary>
    public partial class ColorBox : UserControl
    {
        public ColorBox()
        {
            InitializeComponent();
        }

        
        public Color ColorARGB
        {
            get;set;
        }

        public bool isColor
        {
            get; set;
        } = false;


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
           "Text", typeof(string), typeof(ColorBox),
           new PropertyMetadata(
               "",
               (o, args) => ((ColorBox)o).Update())
           );

        void Update()
        {
            TextBox_color.Text = Text;

            string[] str = Text.Split(new char[] { ',' });
            int r , g , b , a;
            try
            {
                if (int.TryParse(str[0], out a) &&
                int.TryParse(str[1], out r) &&
                int.TryParse(str[0], out g) &&
                int.TryParse(str[2], out b))
                {
                    ColorARGB = Color.FromArgb((byte)a, (byte)r, (byte)g, (byte)b);
                    isColor = true;
                }
            }
            catch (Exception)
            {

                isColor = false;
            }
            

        }
    }
}
