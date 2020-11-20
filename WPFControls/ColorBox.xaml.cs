using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void TextCh(TextChangedEventHandler TextChanged)
        {
            TextBox_color.TextChanged += TextChanged;
        }
            
        public Color ColorARGB
        {
            get;set;
        }


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
            Debug.WriteLine(str.ToString());
            try
            {
                if (int.TryParse(str[0], out int a) &&
                int.TryParse(str[1], out int r) &&
                int.TryParse(str[2], out int g) &&
                int.TryParse(str[3], out int b))
                {
                    ColorARGB = Color.FromArgb((byte)a, (byte)r, (byte)g, (byte)b);
                }
            }
            catch (Exception) { }
        }

        private void TextBox_color_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = TextBox_color.Text;
            Update();
        }
    }
}
