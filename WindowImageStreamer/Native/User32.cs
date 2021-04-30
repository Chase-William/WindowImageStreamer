/*
 * Copyright (c) Chase Roth <cxr6988@rit.edu>
 * Licensed under the MIT License. See repository root directory for more info.
*/

using System;
using System.Runtime.InteropServices;

namespace WindowImageStreamer.Native
{
    /// <summary>
    /// Contains imported User32 functions.
    /// </summary>
    internal static class User32
    {
        /// <summary>
        /// Target DLL for imported functions.
        /// </summary>
        const string USER_32 = "User32";

        // Read Source: https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-printwindow
        [DllImport(USER_32)]
        public static extern bool PrintWindow(
            IntPtr hwnd, // Target window handle
            IntPtr hdcBlt, // Handle to bitmap
            uint nFlags // Target options
        );

        // Read Source: https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowrect
        [DllImport(USER_32)]
        public static extern bool GetWindowRect(
            IntPtr hwnd,
            out LPRect rect
        );

        // Read Source: https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getclientrect
        [DllImport(USER_32)]
        public static extern bool GetClientRect(
            IntPtr hwnd,
            out LPRect rect
        );
    }
}
