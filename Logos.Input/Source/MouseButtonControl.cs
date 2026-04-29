namespace Logos.Input
{
    /// <summary>
    /// Represents a base class for classes that implement mouse button controls whose state can be
    /// changed by mouse button events.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the state held by the <see cref="MouseButtonControl{T}"/>.
    /// </typeparam>
    public abstract class MouseButtonControl<T> : InputControl<T>, IMouseButtonObserver
    {
        /// <summary>
        /// Notifies the <see cref="MouseButtonControl{T}"/> of a mouse button press event.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// An object that contains the event data.
        /// </param>
        public abstract void OnButtonPressed(object? sender, MouseButtonEventArgs e);

        /// <summary>
        /// Notifies the <see cref="MouseButtonControl{T}"/> of a mouse button release event.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// An object that contains the event data.
        /// </param>
        public abstract void OnButtonReleased(object? sender, MouseButtonEventArgs e);
    }
}
