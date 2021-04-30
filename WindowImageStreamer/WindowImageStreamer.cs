/*
 * Copyright (c) Chase Roth <cxr6988@rit.edu>
 * Licensed under the MIT License. See repository root directory for more info.
*/

using System;

using HWND = System.IntPtr;

using WindowImageStreamer.EventArguments;

namespace WindowImageStreamer
{
    public class WindowImageStreamer : WindowImageRetriever
    {
        public WindowImageStreamer(HWND windowHandle, TargetArea area, uint frequency) : base(windowHandle, area)
        {

        }
    }    
}
