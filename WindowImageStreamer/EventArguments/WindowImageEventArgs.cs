/*
 * Copyright (c) Chase Roth <cxr6988@rit.edu>
 * Licensed under the MIT License. See repository root directory for more info.
*/

using HWND = System.IntPtr;

namespace WindowImageStreamer.EventArguments
{
    public class WindowImageEventArgs
    {
        public HWND WindowHandle { get; private set; }

        public WindowImageEventArgs() { }
    }
}
