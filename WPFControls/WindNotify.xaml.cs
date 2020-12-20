using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using TextBox = System.Windows.Controls.TextBox;

namespace WPFControls
{
    public partial class WindNotify : Window
    {
        public  List<TextBox> textBoxes = new List<TextBox>();
        public List<CheckBox> CheckBoxs = new List<CheckBox>();
        public List<ColorBox> ColorBoxs = new List<ColorBox>();
        public bool IsActiveWindow
        {
            get; set;
        }
        public WindNotify()
        {
            InitializeComponent();
            IsActiveWindow = true;

            Label_poswind.Content = string.Format("Position window ScreenSize: {0}x{1}", Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            textBoxes.Add(TextBoxSizeWindow);
            textBoxes.Add(TextBox_Smooth);
            textBoxes.Add(TextBox_sizeline);
            textBoxes.Add(TextBoxPositionWindow);
            textBoxes.Add(TextBoxSmoothing);

            ColorBoxs.Add(Color_box);

            CheckBoxs.Add(CBox);
            CheckBoxs.Add(SmoothingCheckBox);
        }
        
        public void EventTextChanged(TextBox tb, TextChangedEventHandler e , bool d = true) 
        {
            switch (d)
            {
                case true:
                    tb.TextChanged += e;
                    break;
                case false:
                    tb.TextChanged -= e;
                    break;
                default:
                    break;
            }           
        }

        public void EventMouseDownChanged(CheckBox tb, MouseButtonEventHandler e, bool d = true)
        {
            switch (d)
            {
                case true:
                    tb.MouseDown += e;
                    break;
                case false:
                    tb.MouseDown -= e;
                    break;
                default:
                    break;
            }
        }
        public void SetPos()
        {
            Point CurPos = User32Mouse.GetCursorPosition();

            double d = Screen.PrimaryScreen.Bounds.Height - Screen.PrimaryScreen.WorkingArea.Height;
            Debug.WriteLine(d);
            Size sizescreenall = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.Top = (sizescreenall.Height - this.Height) - d;
            this.Left = CurPos.X - this.Width * 0.62;         
        }

        private void Window_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            IsActiveWindow = false;
            this.Hide();
            
        }


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        // private void Window_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) => IsActiveWindow = true;

    }
}
