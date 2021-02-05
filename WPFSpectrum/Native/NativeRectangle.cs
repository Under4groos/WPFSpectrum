using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFSpectrum.Lib
{
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct NativeRectangle
    {
        public Int32 Left;
        public Int32 Top;
        public Int32 Right;
        public Int32 Bottom;


        public NativeRectangle(Int32 left, Int32 top, Int32 right, Int32 bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }
    }

}
