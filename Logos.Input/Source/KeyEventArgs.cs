using System;

namespace Logos.Input
{
    /// <summary>
    /// Provides data for key events sent by keyboard devices.
    /// </summary>
    public class KeyEventArgs : InputEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyEventArgs"/> class that contains event
        /// data sent by the specified keyboard device at the specified time, which was triggered by
        /// the specified key.
        /// </summary>
        /// <param name="device">
        /// The keyboard device that sent the event data.
        /// </param>
        /// <param name="timestamp">
        /// The time at which the event occured.
        /// </param>
        /// <param name="key">
        /// The key that triggered the event.
        /// </param>
        public KeyEventArgs(IKeyboardDevice device, TimeSpan timestamp, KeyCode key) : base(device, timestamp)
        {
            Key = key;
        }

        /// <summary>
        /// Gets the keyboard device that sent the event data.
        /// </summary>
        /// <returns>
        /// The keyboard device that sent the event data.
        /// </returns>
        public new IKeyboardDevice Device
        {
            get => (IKeyboardDevice)base.Device;
        }

        /// <summary>
        /// Gets the key that triggered the event.
        /// </summary>
        /// <returns>
        /// The key that triggered the event.
        /// </returns>
        public KeyCode Key { get; }
    }
}
