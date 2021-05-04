using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;



namespace WIS.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct LPRect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
}
