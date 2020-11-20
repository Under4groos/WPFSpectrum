using System.Collections.Generic;
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

            Label_poswind.Content = string.Format("Position window {0} , {1}", Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            textBoxes.Add(TextBoxSizeWindow);
            textBoxes.Add(TextBox_Smooth);

            ColorBoxs.Add(Color_box);

            CheckBoxs.Add(CBox);
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
  
        
        // CBox
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
            this.Top = (CurPos.Y - this.Height) + 40;
            this.Left = CurPos.X - this.Width * 0.5;         
        }

        private void Window_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            IsActiveWindow = false;
            this.Hide();
            
        }

        // private void Window_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) => IsActiveWindow = true;

    }
}
