/*
 * Copyright (c) Chase Roth <cxr6988@rit.edu>
 * Licensed under the MIT License. See repository root directory for more info.
*/

using System;
using System.Drawing;

using WindowImageStreamer.Native;

namespace WindowImageStreamer
{
    public class WindowImageRetriever
    {        
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
            if (windowHandle == IntPtr.Zero)
                throw new ArgumentNullException($"The parameter {nameof(windowHandle)} cannot be {IntPtr.Zero} or null.");           

            TargetWindowHandle = windowHandle;
            Area = area;
        }

        /// <summary>
        /// Tries to retrieve a bitmap of the target window's image. Uses the <see cref="Area"/> property to determine
        /// which part of the target window should be captured.
        /// </summary>
        /// <param name="bmp">Resulting image of capture. Will be null if failure.</param>
        /// <returns>Indication of success. True == successful, False == failure</returns>
        public bool TryGetWindowImage(out Bitmap bmp)
        {
            bmp = null;
            LPRect rect = default;
            if (Area == TargetArea.EntireWindow) // Get EntireWindow rectangle
            {
                if (!User32.GetWindowRect(TargetWindowHandle, out rect))
                    return false;
            }
            else // Get Client rectangle
            {
                if (!User32.GetClientRect(TargetWindowHandle, out rect))
                    return false;
            }

            if (rect.Equals(default))
                return false;

            bmp = new Bitmap(rect.right - rect.left, rect.bottom - rect.top, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics gfxBmp = Graphics.FromImage(bmp))
            {
                IntPtr hdcBitmap = gfxBmp.GetHdc(); // Get handle to device context
                
                if (!User32.PrintWindow(TargetWindowHandle, hdcBitmap, (uint)Area))
                {
                    bmp.Dispose();
                    bmp = null;
                    return false;
                }
            }
            return true;
        }
    }
}
