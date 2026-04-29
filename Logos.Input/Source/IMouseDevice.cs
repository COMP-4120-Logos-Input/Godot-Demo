using System.Collections.Generic;
using System.Numerics;

namespace Logos.Input
{
    /// <summary>
    /// Defines methods that access the state of a mouse device.
    /// </summary>
    public interface IMouseDevice : IInputDevice
    {
        /// <summary>
        /// Gets the position of the <see cref="IMouseDevice"/>.
        /// </summary>
        /// <returns>
        /// The position of the <see cref="IMouseDevice"/>.
        /// </returns>
        Vector2 Position { get; }

        /// <summary>
        /// Gets the scroll wheel value of the <see cref="IMouseDevice"/>.
        /// </summary>
        /// <returns>
        /// The scroll wheel value of the <see cref="IMouseDevice"/>.
        /// </returns>
        Vector2 ScrollWheel { get; }

        /// <summary>
        /// Gets a collection of mouse buttons pressed on the <see cref="IMouseDevice"/>.
        /// </summary>
        /// <returns>
        /// A collection of mouse buttons pressed on the <see cref="IMouseDevice"/>.
        /// </returns>
        IEnumerable<MouseButton> PressedButtons { get; }

        /// <summary>
        /// Returns a value that indicates whether the specified mouse button is pressed on the
        /// <see cref="IMouseDevice"/>.
        /// </summary>
        /// <param name="button">
        /// The mouse button to check.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="button"/> is pressed on the
        /// <see cref="IMouseDevice"/>; otherwise, <see langword="false"/>.
        /// </returns>
        bool IsButtonPressed(MouseButton button);
    }
}
