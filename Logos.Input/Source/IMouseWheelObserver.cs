namespace Logos.Input
{
    /// <summary>
    /// Provides mechanisms for receiving mouse wheel event data.
    /// </summary>
    public interface IMouseWheelObserver
    {
        /// <summary>
        /// Notifies the <see cref="IMouseWheelObserver"/> of a mouse wheel event.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// An object that contains the event data.
        /// </param>
        void OnWheelMoved(object? sender, MouseWheelEventArgs e);
    }
}
