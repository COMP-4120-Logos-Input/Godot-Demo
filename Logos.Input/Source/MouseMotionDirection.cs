using System;

namespace Logos.Input
{
    /// <summary>
    /// Specifies bitflags for mouse motion directions. 
    /// </summary>
    [Flags]
    public enum MouseMotionDirection
    {
        Any = 0,
        Up = 1 << 0,
        Down = 1 << 1,
        Left = 1 << 2,
        Right = 1 << 3
    }
}
