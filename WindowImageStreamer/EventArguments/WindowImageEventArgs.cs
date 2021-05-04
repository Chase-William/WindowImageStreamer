/*
 * Copyright (c) Chase Roth <cxr6988@rit.edu>
 * Licensed under the MIT License. See repository root directory for more info.
*/

using System.Drawing;
using HWND = System.IntPtr;

namespace WIS.EventArguments
{
    /// <summary>
    /// A <see cref="WindowImageEventArgs"/> class that contains a window handle and a retrieved bitmap.
    /// </summary>
    public class WindowImageEventArgs
    {
        /// <summary>
        /// Handle to window that the <see cref="Image"/> was retrieved from.
        /// </summary>
        public HWND WindowHandle { get; set; }
        /// <summary>
        /// Image retrieved.
        /// </summary>
        public Bitmap Image { get; set; }

        public WindowImageEventArgs() { }

        public WindowImageEventArgs(HWND hwnd, Bitmap bmp)
        {
            WindowHandle = hwnd;
            Image = bmp;
        }
    }
}
