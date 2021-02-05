using System;
using System.Runtime.InteropServices;
using WPFSpectrum.Lib;

namespace WPFSpectrum.Native
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public sealed class NativeMonitorInfo
    {
        public Int32 Size = Marshal.SizeOf(typeof(NativeMonitorInfo));
        public NativeRectangle Monitor;
        public NativeRectangle Work;
        public Int32 Flags;
    }
}
