/*
 * Copyright (c) Chase Roth <cxr6988@rit.edu>
 * Licensed under the MIT License. See repository root directory for more info.
*/

using System.Drawing;
using HWND = System.IntPtr;

namespace WindowImageStreamer.EventArguments
{
    public class WindowImageEventArgs
    {
        public HWND WindowHandle { get; set; }
        public Bitmap Image { get; set; }

        public WindowImageEventArgs() { }
        public WindowImageEventArgs(HWND hwnd, Bitmap bmp)
        {
            WindowHandle = hwnd;
            Image = bmp;
        }
    }
}
