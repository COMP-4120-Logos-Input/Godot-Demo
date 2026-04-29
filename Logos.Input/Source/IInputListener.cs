using System;
using System.Collections.Generic;

namespace Logos.Input
{
    /// <summary>
    /// Defines methods that access connected input devices and notify event handlers when their
    /// connection status changes.
    /// </summary>
    public interface IInputListener
    {
        /// <summary>
        /// Gets a collection of input devices that are currently connected.
        /// </summary>
        /// <returns>
        /// A collection of input devices that are currently connected.
        /// </returns>
        IEnumerable<IInputDevice> Devices { get; }

        /// <summary>
        /// Occurs when an input device is connected.
        /// </summary>
        event EventHandler<InputEventArgs> DeviceConnected;

        /// <summary>
        /// Occurs when an input device is disconnected.
        /// </summary>
        event EventHandler<InputEventArgs> DeviceDisconnected;
    }
}
