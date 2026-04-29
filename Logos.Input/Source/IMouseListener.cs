using System;
using System.Collections.Generic;

namespace Logos.Input
{
    /// <summary>
    /// Defines methods that access connected mouse devices and notify event handlers when their
    /// states have changed.
    /// </summary>
    public interface IMouseListener : IInputListener
    {
        /// <summary>
        /// Gets a collection of mouse devices that are currently connected.
        /// </summary>
        /// <returns>
        /// A collection of mouse devices that are currently connected.
        /// </returns>
        new IEnumerable<IMouseDevice> Devices { get; }

        /// <summary>
        /// Occurs when a button has been pressed on a mouse device.
        /// </summary>
        event EventHandler<MouseButtonEventArgs> ButtonPressed;

        /// <summary>
        /// Occurs when a button has been released on a mouse device.
        /// </summary>
        event EventHandler<MouseButtonEventArgs> ButtonReleased;

        /// <summary>
        /// Occurs when a mouse device is moved.
        /// </summary>
        event EventHandler<MouseMotionEventArgs> MouseMoved;

        /// <summary>
        /// Occurs when a scroll wheel is moved on a mouse device.
        /// </summary>
        event EventHandler<MouseWheelEventArgs> WheelMoved;
    }
}
