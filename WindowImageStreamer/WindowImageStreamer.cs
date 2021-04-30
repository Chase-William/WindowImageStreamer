/*
 * Copyright (c) Chase Roth <cxr6988@rit.edu>
 * Licensed under the MIT License. See repository root directory for more info.
*/

using System;
using System.Timers;
using HWND = System.IntPtr;

using WindowImageStreamer.EventArguments;
using System.Drawing;

namespace WindowImageStreamer
{
    public class WindowImageStreamer : WindowImageRetriever
    {
        /// <summary>
        /// Raised when a new image is received from the target window.
        /// </summary>
        public event EventHandler<WindowImageEventArgs> ImageReceived;

        private readonly Timer timer;

        public WindowImageStreamer(HWND windowHandle, TargetArea area, uint milliFrequency) : base(windowHandle, area)
        {
            timer = new Timer(milliFrequency);
            timer.Elapsed += Timer_Elapsed;
        }

        /// <summary>
        /// Raises the <see cref="ImageReceived"/> event when an image is polled from the target window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TryGetWindowImage(out Bitmap bmp);       
            ImageReceived?.Invoke(this, new WindowImageEventArgs(TargetWindowHandle, bmp));
        }
    }    
}
