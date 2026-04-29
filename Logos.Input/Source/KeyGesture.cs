namespace Logos.Input
{
    /// <summary>
    /// Represents an association between keys and key actions.
    /// </summary>
    public readonly record struct KeyGesture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyGesture"/> structure to the specified
        /// key and key action.
        /// </summary>
        /// <param name="key">
        /// The key associated with the <see cref="KeyGesture"/>.
        /// </param>
        /// <param name="action">
        /// The key action associated with the <see cref="KeyGesture"/>.
        /// </param>
        public KeyGesture(KeyCode key, KeyAction action)
        {
            Key = key;
            Action = action;
        }

        /// <summary>
        /// Gets the key associated with the <see cref="KeyGesture"/>.
        /// </summary>
        /// <returns>
        /// The key associated with the <see cref="KeyGesture"/>.
        /// </returns>
        public KeyCode Key { get; }

        /// <summary>
        /// Gets the key action associated with the <see cref="KeyGesture"/>.
        /// </summary>
        /// <returns>
        /// The key action associated with the <see cref="KeyGesture"/>.
        /// </returns>
        public KeyAction Action { get; }
    }
}
