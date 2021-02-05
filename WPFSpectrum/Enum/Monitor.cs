using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSpectrum.Enum
{
    public static class Monitor
    {
        public enum MonitorDpiType
        {
            MDT_EFFECTIVE_DPI = 0,
            MDT_ANGULAR_DPI = 1,
            MDT_RAW_DPI = 2
        }
        public enum MonitorFlag : uint
        {
            MONITOR_DEFAULTTONULL = 0,         
            MONITOR_DEFAULTTOPRIMARY = 1,            
            MONITOR_DEFAULTTONEAREST = 2
        }

    }
}
