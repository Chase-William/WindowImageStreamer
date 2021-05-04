/*
 * Copyright (c) Chase Roth <cxr6988@rit.edu>
 * Licensed under the MIT License. See repository root directory for more info.
*/

namespace WIS
{
    /// <summary>
    /// The area of a window to retrieve.
    /// </summary>
    public enum TargetArea : uint
    {
        /// <summary>
        /// Streams the client area and the non-client area.
        /// </summary>
        EntireWindow,
        /// <summary>
        /// Streams only the client area.
        /// </summary>
        OnlyClientArea
    }
}
