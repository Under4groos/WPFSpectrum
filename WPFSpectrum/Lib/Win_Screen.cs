using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using WPFSpectrum.Native;

namespace WPFSpectrum.Lib
{
    class Win_Screen
    {

        private const Int32 MONITOR_DEFAULTTOPRIMERTY = 0x00000001;
        private const Int32 MONITOR_DEFAULTTONEAREST = 0x00000002;
        private Size screen_Size;
        private Size center_screen_Size;
        //private IntPtr hwnd;
        //private IntPtr monitor;
        //private NativeMonitorInfo monitorInfo;


        public Win_Screen(Window w)
        {
            IntPtr monitor = API.MonitorFromWindow(new WindowInteropHelper(w).EnsureHandle(), MONITOR_DEFAULTTONEAREST);
            if (monitor != IntPtr.Zero)
            {
                var monitorInfo = new NativeMonitorInfo();
                API.GetMonitorInfo(monitor, monitorInfo);

                //var left = monitorInfo.Monitor.Left;
                //var top = monitorInfo.Monitor.Top;
                var width = (monitorInfo.Monitor.Right - monitorInfo.Monitor.Left);
                var height = (monitorInfo.Monitor.Bottom - monitorInfo.Monitor.Top);
                ScreenSize = new Size(width, height);
            }
        }
        public Size ScreenSize
        {
            get
            {
                return screen_Size;
            }
            set
            {
                screen_Size = value;
            }
        }
        public Size center_ScreenSize
        {
            get
            {
                return screen_Size / 2;
            }
            set
            {
                screen_Size = value;
            }
        }









        //public Size GetScreenSize()
        //{
            
           
        //    var width = 0;
        //    var height = 0;
        //    if (monitor != IntPtr.Zero)
        //    {
                

        //        var left = monitorInfo.Monitor.Left;
        //        var top = monitorInfo.Monitor.Top;
        //        width = (monitorInfo.Monitor.Right - monitorInfo.Monitor.Left);
        //        height = (monitorInfo.Monitor.Bottom - monitorInfo.Monitor.Top);
        //    }

        //    return new Size(width, height);
        //}

    }
}
