using System;
using System.Collections.Generic;
using System.Numerics;

namespace Logos.Input
{
    /// <summary>
    /// Represents an event router that notifies mouse observers of events triggered by mapped mouse
    /// gestures.
    /// </summary>
    public class MouseMapper : IInputMapper
    {
        private const int UpDirectionFlag = 1 << 0;
        private const int DownDirectionFlag = 1 << 1;
        private const int LeftDirectionFlag = 1 << 2;
        private const int RightDirectionFlag = 1 << 3;

        private readonly IMouseListener _listener;
        private readonly Dictionary<MouseButtonGesture, IMouseButtonObserver> _buttonBindings;
        private readonly Dictionary<MouseMotionDirection, IMouseMotionObserver> _motionBindings;
        private readonly Dictionary<MouseWheelDirection, IMouseWheelObserver> _wheelBindings;
        private bool _isEnabled;

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseMapper"/> class that routes events
        /// sent by the specified mouse listener to key observers when enabled.
        /// </summary>
        /// <param name="listener">
        /// The mouse listener whose events are to be routed.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="listener"/> is <see langword="null"/>.
        /// </exception>
        public MouseMapper(IMouseListener listener)
        {
            ArgumentNullException.ThrowIfNull(listener);
            _listener =  listener;
            _buttonBindings = new Dictionary<MouseButtonGesture, IMouseButtonObserver>();
            _motionBindings = new Dictionary<MouseMotionDirection, IMouseMotionObserver>();
            _wheelBindings = new Dictionary<MouseWheelDirection, IMouseWheelObserver>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseMapper"/> class that routes events
        /// sent by the specified mouse listener to key observers when enabled.
        /// </summary>
        /// <param name="listener">
        /// The mouse listener whose events are to be routed.
        /// </param>
        /// <param name="isEnabled">
        /// Enables event routing if <see langword="true"/>; otherwise, disables event routing.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="listener"/> is <see langword="null"/>.
        /// </exception>
        public MouseMapper(IMouseListener listener, bool isEnabled)
        {
            ArgumentNullException.ThrowIfNull(listener);

            if (isEnabled)
            {
                listener.ButtonPressed += OnButtonPressed;
                listener.ButtonReleased += OnButtonReleased;
                listener.MouseMoved += OnMouseMoved;
                listener.WheelMoved += OnWheelMoved;
            }

            _listener =  listener;
            _isEnabled = isEnabled;
            _buttonBindings = new Dictionary<MouseButtonGesture, IMouseButtonObserver>();
            _motionBindings = new Dictionary<MouseMotionDirection, IMouseMotionObserver>();
            _wheelBindings = new Dictionary<MouseWheelDirection, IMouseWheelObserver>();
        }

        /// <summary>
        /// Gets the mouse listener whose events are to be routed.
        /// </summary>
        /// <returns>
        /// The mouse listener whose events are to be routed.
        /// </returns>
        public IMouseListener Listener
        {
            get => _listener;
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the <see cref="MouseMapper"/> is routing
        /// events to mapped mouse observers.
        /// </summary>
        /// <param name="value">
        /// Enables event routing if <see langword="true"/>; otherwise, disables event routing.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if event routing is enabled; otherwise, <see langword="false"/>.
        /// </returns>
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled == value)
                {
                    return;
                }

                if (value)
                {
                    _listener.ButtonPressed += OnButtonPressed;
                    _listener.ButtonReleased += OnButtonReleased;
                    _listener.MouseMoved += OnMouseMoved;
                    _listener.WheelMoved += OnWheelMoved;
                }
                else
                {
                    _listener.ButtonPressed -= OnButtonPressed;
                    _listener.ButtonReleased -= OnButtonReleased;
                    _listener.MouseMoved -= OnMouseMoved;
                    _listener.WheelMoved -= OnWheelMoved;
                }

                _isEnabled = value;
                EnabledChanged?.Invoke(this, value);
            }
        }

        /// <summary>
        /// Occurs when the <see cref="MouseMapper"/> is enabled or disabled.
        /// </summary>
        public event EventHandler<bool>? EnabledChanged;

        /// <summary>
        /// Routes input events triggered by the specified mouse button gesture to the specified
        /// mouse button observer.
        /// </summary>
        /// <param name="gesture">
        /// The mouse button gesture to bind.
        /// </param>
        /// <param name="observer">
        /// The mouse button observer that will listen for input events triggered by
        /// <paramref name="gesture"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="observer"/> is <see langword="null"/>.
        /// </exception>
        public void Bind(MouseButtonGesture gesture, IMouseButtonObserver observer)
        {
            ArgumentNullException.ThrowIfNull(observer);
            _buttonBindings.Add(gesture, observer);
        }

        /// <summary>
        /// Routes input events triggered by the specified mouse motion direction to the specified
        /// mouse motion observer.
        /// </summary>
        /// <param name="direction">
        /// The mouse motion direction to bind.
        /// </param>
        /// <param name="observer">
        /// The mouse motion observer that will listen for input events triggered by mouse motion
        /// in <paramref name="direction"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="observer"/> is <see langword="null"/>.
        /// </exception>
        public void Bind(MouseMotionDirection direction, IMouseMotionObserver observer)
        {
            ArgumentNullException.ThrowIfNull(observer);
            _motionBindings.Add(direction, observer);
        }

        /// <summary>
        /// Routes input events triggered by the specified mouse wheel scroll direction to the
        /// specified mouse wheel observer.
        /// </summary>
        /// <param name="direction">
        /// The mouse wheel scroll direction to bind.
        /// </param>
        /// <param name="observer">
        /// The mouse wheel observer that will listen for input events triggered by mouse wheel
        /// scrolling in <paramref name="direction"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="observer"/> is <see langword="null"/>.
        /// </exception>
        public void Bind(MouseWheelDirection direction, IMouseWheelObserver observer)
        {
            ArgumentNullException.ThrowIfNull(observer);
            _wheelBindings.Add(direction, observer);
        }

        /// <summary>
        /// Removes the mouse button observer that was listening for input events triggered by the
        /// specified mouse button gesture.
        /// </summary>
        /// <param name="gesture">
        /// The mouse button gesture to unbind.
        /// </param>
        public void Unbind(MouseButtonGesture gesture)
        {
            _buttonBindings.Remove(gesture);
        }

        /// <summary>
        /// Removes the mouse motion observer that was listening for input events triggered by mouse
        /// motion in the specified direction.
        /// </summary>
        /// <param name="direction">
        /// The mouse motion direction to unbind.
        /// </param>
        public void Unbind(MouseMotionDirection direction)
        {
            _motionBindings.Remove(direction);
        }

        /// <summary>
        /// Removes the mouse wheel observer that was listening for input events triggered by mouse
        /// wheel scrolling in the specified direction.
        /// </summary>
        /// <param name="direction">
        /// The mouse wheel scroll direction to unbind.
        /// </param>
        public void Unbind(MouseWheelDirection direction)
        {
            _wheelBindings.Remove(direction);
        }

        private void OnButtonPressed(object? sender, MouseButtonEventArgs args)
        {
            MouseButtonGesture gesture = new MouseButtonGesture(args.Button, MouseButtonAction.Press);

            if (_buttonBindings.TryGetValue(gesture, out IMouseButtonObserver? observer))
            {
                observer.OnButtonPressed(sender, args);
            }
        }

        private void OnButtonReleased(object? sender, MouseButtonEventArgs args)
        {
            MouseButtonGesture gesture = new MouseButtonGesture(args.Button, MouseButtonAction.Release);

            if (_buttonBindings.TryGetValue(gesture, out IMouseButtonObserver? observer))
            {
                observer.OnButtonReleased(sender, args);
            }
        }

        private void OnMouseMoved(object? sender, MouseMotionEventArgs args)
        {
            if (_motionBindings.TryGetValue(MouseMotionDirection.Any, out IMouseMotionObserver? observer))
            {
                observer.OnMouseMoved(sender, args);
            }

            MouseMotionDirection direction = (MouseMotionDirection)GetDirection(args.Velocity);

            if (direction != 0 && _motionBindings.TryGetValue(direction, out observer))
            {
                observer.OnMouseMoved(sender, args);
            }
        }

        private void OnWheelMoved(object? sender, MouseWheelEventArgs args)
        {
            if (_wheelBindings.TryGetValue(MouseWheelDirection.Any, out IMouseWheelObserver? observer))
            {
                observer.OnWheelMoved(sender, args);
            }

            MouseWheelDirection direction = (MouseWheelDirection)GetDirection(args.Scroll);

            if (direction != 0 && _wheelBindings.TryGetValue(direction, out observer))
            {
                observer.OnWheelMoved(sender, args);
            }
        }

        private static int GetDirection(Vector2 vector)
        {
            int flags = 0;

            if (vector.Y > 0.0f)
            {
                flags |= UpDirectionFlag;
            }
            else if (vector.Y < 0.0f)
            {
                flags |= DownDirectionFlag;
            }

            if (vector.X > 0.0f)
            {
                flags |= LeftDirectionFlag;
            }
            else if (vector.X < 0.0f)
            {
                flags |= RightDirectionFlag;
            }

            return flags;
        }
    }
}
