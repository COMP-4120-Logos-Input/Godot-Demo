using System;
using System.Numerics;

namespace Logos.Input
{
    /// <summary>
    /// Provides data for mouse wheel events sent by mouse devices.
    /// </summary>
    public class MouseWheelEventArgs : InputEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseWheelEventArgs"/> class that contains
        /// event data sent by the specified mouse device at the specified time, which was triggered
        /// by the specified change in the mouse device's scroll wheel value.
        /// </summary>
        /// <param name="device">
        /// The mouse device that sent the event.
        /// </param>
        /// <param name="timestamp">
        /// The time at which the event occured.
        /// </param>
        /// <param name="scroll">
        /// The change in the scroll wheel value of <paramref name="device"/> that triggered the
        /// event.
        /// </param>
        public MouseWheelEventArgs(IMouseDevice device, TimeSpan timestamp, Vector2 scroll) : base(device, timestamp)
        {
            Scroll = scroll;
        }

        /// <summary>
        /// Gets the mouse device that sent the event data.
        /// </summary>
        /// <returns>
        /// The mouse device that sent the event data.
        /// </returns>
        public new IMouseDevice Device
        {
            get => (IMouseDevice)base.Device;
        }

        /// <summary>
        /// Gets the change in the scroll wheel value of <see cref="Device"/> that triggered the
        /// event.
        /// </summary>
        /// <returns>
        /// The change in the scroll wheel's value on <see cref="Device"/> that triggered the event.
        /// </returns>
        public Vector2 Scroll { get; }
    }
}
