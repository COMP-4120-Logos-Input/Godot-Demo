namespace Logos.Input
{
    /// <summary>
    /// Represents a base class for classes that implement mouse motion controls whose state can be
    /// changed by mouse motion events.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the state held by the <see cref="MouseMotionControl{T}"/>.
    /// </typeparam>
    public abstract class MouseMotionControl<T> : InputControl<T>, IMouseMotionObserver
    {
        /// <summary>
        /// Notifies the <see cref="MouseMotionControl{T}"/> of a mouse move event.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// An object that contains the event data.
        /// </param>
        public abstract void OnMouseMoved(object? sender, MouseMotionEventArgs e);
    }
}
