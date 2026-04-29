namespace Logos.Input
{
    /// <summary>
    /// Provides mechanisms for receiving key event data.
    /// </summary>
    public interface IKeyObserver
    {
        /// <summary>
        /// Notifies the <see cref="IKeyObserver"/> of a key press event.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// An object that contains the event data.
        /// </param>
        void OnKeyPressed(object? sender, KeyEventArgs e);

        /// <summary>
        /// Notifies the <see cref="IKeyObserver"/> of a key repeat event.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// An object that contains the event data.
        /// </param>
        void OnKeyRepeated(object? sender, KeyEventArgs e);

        /// <summary>
        /// Notifies the <see cref="IKeyObserver"/> of a key release event.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// An object that contains the event data.
        /// </param>
        void OnKeyReleased(object? sender, KeyEventArgs e);
    }
}
