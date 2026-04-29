using System;
using System.Collections.Generic;

namespace Logos.Input
{
    /// <summary>
    /// Defines methods that access connected keyboard devices and notify event handlers when their
    /// states have changed.
    /// </summary>
    public interface IKeyboardListener : IInputListener
    {
        /// <summary>
        /// Gets a collection of keyboard devices that are currently connected.
        /// </summary>
        /// <returns>
        /// A collection of keyboard devices that are currently connected.
        /// </returns>
        new IEnumerable<IKeyboardDevice> Devices { get; }

        /// <summary>
        /// Occurs when a key is pressed on a keyboard device.
        /// </summary>
        event EventHandler<KeyEventArgs> KeyPressed;

        /// <summary>
        /// Occurs when a key is held down on a keyboard device.
        /// </summary>
        event EventHandler<KeyEventArgs> KeyRepeated;

        /// <summary>
        /// Occurs when a key is released on a keyboard device.
        /// </summary>
        event EventHandler<KeyEventArgs> KeyReleased;
    }
}
