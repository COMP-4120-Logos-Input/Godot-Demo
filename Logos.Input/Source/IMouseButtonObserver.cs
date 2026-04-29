namespace Logos.Input
{
    /// <summary>
    /// Provides mechanisms for receiving mouse button event data.
    /// </summary>
    public interface IMouseButtonObserver
    {
        /// <summary>
        /// Notifies the <see cref="IMouseButtonObserver"/> of a button press event.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// An object that contains the event data.
        /// </param>
        void OnButtonPressed(object? sender, MouseButtonEventArgs e);

        /// <summary>
        /// Notifies the <see cref="IMouseButtonObserver"/> of a button release event.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// An object that contains the event data.
        /// </param>
        void OnButtonReleased(object? sender, MouseButtonEventArgs e);
    }
}
