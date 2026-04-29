namespace Logos.Input
{
    /// <summary>
    /// Represents an association between mouse buttons and mouse button actions.
    /// </summary>
    public readonly record struct MouseButtonGesture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseButtonGesture"/> structure to the
        /// specified mouse button and mouse button action.
        /// </summary>
        /// <param name="button">
        /// The mouse button associated with the <see cref="MouseButtonGesture"/>.
        /// </param>
        /// <param name="action">
        /// The mouse button action associated with the <see cref="MouseButtonGesture"/>.
        /// </param>
        public MouseButtonGesture(MouseButton button, MouseButtonAction action)
        {
            Button = button;
            Action = action;
        }

        /// <summary>
        /// Gets the mouse button associated with the <see cref="MouseButtonGesture"/>.
        /// </summary>
        /// <returns>
        /// The mouse button associated with the <see cref="MouseButtonGesture"/>.
        /// </returns>
        public MouseButton Button { get; }

        /// <summary>
        /// Gets the mouse button action associated with the <see cref="MouseButtonGesture"/>.
        /// </summary>
        /// <returns>
        /// The mouse button action associated with the <see cref="MouseButtonGesture"/>.
        /// </returns>
        public MouseButtonAction Action { get; }
    }
}
