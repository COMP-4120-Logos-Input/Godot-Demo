using System;

namespace Logos.Input
{
    /// <summary>
    /// Represents a base class for classes that implement input controls whose state can be changed
    /// by input events.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the state held by the <see cref="InputControl{T}"/>.
    /// </typeparam>
    public abstract class InputControl<T>
    {
        private T? _state;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputControl{T}"/> class to the default
        /// state.
        /// </summary>
        protected InputControl()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputControl{T}"/> class to the specified
        /// state.
        /// </summary>
        /// <param name="state">
        /// The initial state of the <see cref="InputControl{T}"/>.
        /// </param>
        protected InputControl(T state)
        {
            _state = state;
        }

        /// <summary>
        /// Gets or sets the state of the <see cref="InputControl{T}"/>.
        /// </summary>
        /// <param name="value">
        /// The new state of the <see cref="InputControl{T}"/>.
        /// </param>
        /// <returns>
        /// The state of the <see cref="InputControl{T}"/>.
        /// </returns>
        public T State
        {
            get => _state!;
            protected set
            {
                _state = value;
                StateChanged?.Invoke(this, value);
            }
        }

        /// <summary>
        /// Occurs when the state of the <see cref="InputControl{T}"/> is changed.
        /// </summary>
        public event EventHandler<T>? StateChanged;
    }
}
