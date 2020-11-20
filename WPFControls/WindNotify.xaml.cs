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
        public WindNotify()
        {
            InitializeComponent();


            Label_poswind.Content = string.Format("Position window {0} , {1}", Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            textBoxes.Add(TextBoxSizeWindow);
            textBoxes.Add(TextBox_Smooth);
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


        public void SetPos()
        {
            Point CurPos = User32Mouse.GetCursorPosition();
            this.Top = (CurPos.Y - this.Height) + 40;
            this.Left = CurPos.X - this.Width * 0.5;
        }

        private void Window_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Hide();
        }
    }
}
