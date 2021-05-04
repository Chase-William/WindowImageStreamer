/*
 * Copyright (c) Chase Roth <cxr6988@rit.edu>
 * Licensed under the MIT License. See repository root directory for more info.
*/

using System;
using System.Timers;
using HWND = System.IntPtr;

using WIS.EventArguments;
using System.Drawing;

namespace WIS
{
    /// <summary>
    /// A <see cref="WindowImageStreamer"/> class for retrieving bitmaps from a target window at a specified rate.
    /// </summary>
    public class WindowImageStreamer : WindowImageRetriever
    {
        /// <summary>
        /// Raised when a new image is received from the target window. 
        /// This event will not fire when retrieval of the bitmap from the target window fails.
        /// Subscribe to <see cref="ImageRetrievalError"/> to be notified of these failures.
        /// </summary>
        public event EventHandler<WindowImageEventArgs> ImageReceived;
        /// <summary>
        /// Raised when a new bitmap was requested and null was received.
        /// </summary>
        public event EventHandler ImageRetrievalError;

        private readonly Timer timer;

        public WindowImageStreamer(HWND windowHandle, TargetArea area, double milliFrequency) : base(windowHandle, area)
        {
            timer = new Timer(milliFrequency);
            timer.Elapsed += Timer_Elapsed;
        }

        /// <summary>
        /// Gets and sets the interval to retrieve the bitmaps at.
        /// </summary>
        public double Interval
        {
            get => timer.Interval;
            set => timer.Interval = value;
        }

        /// <summary>
        /// Starts streaming the window.
        /// </summary>
        public void Start() => timer.Start();

        /// <summary>
        /// Stops streaming the window.
        /// </summary>
        public void Stop() => timer.Stop();     

        /// <summary>
        /// Raises the <see cref="ImageReceived"/> event when an image is polled from the target window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!TryGetWindowImage(out Bitmap bmp))
            {   // Notify error
                ImageRetrievalError?.Invoke(this, EventArgs.Empty);
                return;
            }
            ImageReceived?.Invoke(this, new WindowImageEventArgs(TargetWindowHandle, bmp));
            bmp.Dispose();
        }
    }    
}
