namespace Logos.Input
{
    /// <summary>
    /// Provides mechanisms for receiving mouse motion event data.
    /// </summary>
    public interface IMouseMotionObserver
    {
        /// <summary>
        /// Notifies the <see cref="IMouseMotionObserver"/> of a mouse motion event.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// An object that contains the event data.
        /// </param>
        void OnMouseMoved(object? sender, MouseMotionEventArgs e);
    }
}
