using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WPFSpectrum.Enum;

namespace WPFSpectrum
{
    static class API
    {
        //[DllImport("user32.dll")]
        //public static extern IntPtr MonitorFromWindow(IntPtr hwnd, Monitor.MonitorFlag flag);

        //[DllImport("user32.dll")]
        //public static extern bool GetDpiForMonitor(IntPtr hwnd, Monitor.MonitorDpiType dpiType, out int dpiX, out int dpiY);

        //[DllImport("user32.dll", SetLastError = false)]
        //public static extern IntPtr GetDesktopWindow();


        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr handle, Int32 flags);


        [DllImport("user32.dll")]
        public static extern Boolean GetMonitorInfo(IntPtr hMonitor, WPFSpectrum.Native.NativeMonitorInfo lpmi);

    }
}
