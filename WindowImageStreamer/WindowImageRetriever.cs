/*
 * Copyright (c) Chase Roth <cxr6988@rit.edu>
 * Licensed under the MIT License. See repository root directory for more info.
*/

using System;

using WindowImageStreamer.EventArguments;
using System.Drawing;

namespace WindowImageStreamer
{
    public class WindowImageRetriever : IDisposable
    {
        /// <summary>
        /// Raised when a new image is received from the target window.
        /// </summary>
        public event EventHandler<WindowImageEventArgs> ImageReceived;

        /// <summary>
        /// Gets the target window being retrieved.
        /// </summary>
        public IntPtr TargetWindowHandle { get; private set; }
        /// <summary>
        /// Gets the target area of the window being retrieved.
        /// </summary>
        public TargetArea Area { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="WindowImageStreamer"/> instance.
        /// </summary>
        /// <param name="windowHandle">Target window to retrieve.</param>
        /// <param name="area">Area of the target window to retrieve.</param>
        public WindowImageRetriever(IntPtr windowHandle, TargetArea area)
        {
            TargetWindowHandle = windowHandle;
            Area = area;
        }

        public bool TryGetWindowImage(out Bitmap bmp)
        {
            bmp = new Bitmap(0,0, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc(); // Get handle to device context
           
            if (!Native.User32.PrintWindow(TargetWindowHandle, hdcBitmap, (uint)Area))
            {
                gfxBmp.ReleaseHdc();
                gfxBmp.Dispose();
                return false;
            }



            return true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
