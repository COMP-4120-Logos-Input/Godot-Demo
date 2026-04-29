using System.Collections.Generic;

namespace Logos.Input
{
    /// <summary>
    /// Defines methods that access the state of a keyboard device.
    /// </summary>
    public interface IKeyboardDevice : IInputDevice
    {
        /// <summary>
        /// Gets a collection of keys pressed on the <see cref="IKeyboardDevice"/>.
        /// </summary>
        /// <returns>
        /// A collection of keys pressed on the <see cref="IKeyboardDevice"/>.
        /// </returns>
        IEnumerable<KeyCode> PressedKeys { get; }

        /// <summary>
        /// Returns a value that indicates whether the specified key is pressed on the
        /// <see cref="IKeyboardDevice"/>.
        /// </summary>
        /// <param name="key">
        /// The key to check.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="key"/> is pressed on the
        /// <see cref="IKeyboardDevice"/>; otherwise, <see langword="false"/>.
        /// </returns>
        bool IsKeyPressed(KeyCode key);
    }
}
