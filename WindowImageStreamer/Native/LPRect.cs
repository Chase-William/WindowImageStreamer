using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;



namespace WindowImageStreamer.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct LPRect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
}
