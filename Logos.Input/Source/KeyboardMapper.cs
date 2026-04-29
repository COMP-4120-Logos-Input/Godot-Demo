using System;
using System.Collections.Generic;

namespace Logos.Input
{
    /// <summary>
    /// Represents an event router that notifies key observers of events triggered by mapped key
    /// gestures.
    /// </summary>
    public class KeyboardMapper : IInputMapper
    {
        private readonly IKeyboardListener _listener;
        private readonly Dictionary<KeyGesture, IKeyObserver> _bindings;
        private bool _isEnabled;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardMapper"/> class that routes events
        /// sent by the specified keyboard listener to key observers when enabled.
        /// </summary>
        /// <param name="listener">
        /// The keyboard listener whose events are to be routed.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="listener"/> is <see langword="null"/>.
        /// </exception>
        public KeyboardMapper(IKeyboardListener listener)
        {
            ArgumentNullException.ThrowIfNull(listener);
            _listener =  listener;
            _bindings = new Dictionary<KeyGesture, IKeyObserver>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardMapper"/> class that routes events
        /// sent by the specified keyboard listener to key observers when enabled.
        /// </summary>
        /// <param name="listener">
        /// The keyboard listener whose events are to be routed.
        /// </param>
        /// <param name="isEnabled">
        /// Enables event routing if <see langword="true"/>; otherwise, disables event routing.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="listener"/> is <see langword="null"/>.
        /// </exception>
        public KeyboardMapper(IKeyboardListener listener, bool isEnabled)
        {
            ArgumentNullException.ThrowIfNull(listener);

            if (isEnabled)
            {
                listener.KeyPressed += OnKeyPressed;
                listener.KeyRepeated += OnKeyRepeated;
                listener.KeyReleased += OnKeyReleased;
            }

            _listener =  listener;
            _isEnabled = isEnabled;
            _bindings = new Dictionary<KeyGesture, IKeyObserver>();
        }

        /// <summary>
        /// Gets the keyboard listener whose events are to be routed.
        /// </summary>
        /// <returns>
        /// The keyboard listener whose events are to be routed.
        /// </returns>
        public IKeyboardListener Listener
        {
            get => _listener;
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the <see cref="KeyboardMapper"/> is routing
        /// events to mapped key observers.
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
                    _listener.KeyPressed += OnKeyPressed;
                    _listener.KeyRepeated += OnKeyRepeated;
                    _listener.KeyReleased += OnKeyReleased;
                }
                else
                {
                    _listener.KeyPressed -= OnKeyPressed;
                    _listener.KeyRepeated -= OnKeyRepeated;
                    _listener.KeyReleased -= OnKeyReleased;
                }

                _isEnabled = value;
                EnabledChanged?.Invoke(this, value);
            }
        }

        /// <summary>
        /// Occurs when the <see cref="KeyboardMapper"/> is enabled or disabled.
        /// </summary>
        public event EventHandler<bool>? EnabledChanged;

        /// <summary>
        /// Routes input events triggered by the specified key gesture to the specified key
        /// observer.
        /// </summary>
        /// <param name="gesture">
        /// The key gesture to bind.
        /// </param>
        /// <param name="observer">
        /// The key observer that will listen for input events triggered by
        /// <paramref name="gesture"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="observer"/> is <see langword="null"/>.
        /// </exception>
        public void Bind(KeyGesture gesture, IKeyObserver observer)
        {
            ArgumentNullException.ThrowIfNull(observer);
            _bindings[gesture] = observer;
        }

        /// <summary>
        /// Removes the key observer that was listening for input events triggered by the specified
        /// key gesture.
        /// </summary>
        /// <param name="gesture">
        /// The key gesture to unbind.
        /// </param>
        public void Unbind(KeyGesture gesture)
        {
            _bindings.Remove(gesture);
        }

        private void OnKeyPressed(object? sender, KeyEventArgs args)
        {
            KeyGesture gesture = new KeyGesture(args.Key, KeyAction.Press);

            if (_bindings.TryGetValue(gesture, out IKeyObserver? observer))
            {
                observer.OnKeyPressed(sender, args);
            }
        }

        private void OnKeyRepeated(object? sender, KeyEventArgs args)
        {
            KeyGesture gesture = new KeyGesture(args.Key, KeyAction.Repeat);

            if (_bindings.TryGetValue(gesture, out IKeyObserver? observer))
            {
                observer.OnKeyRepeated(sender, args);
            }
        }

        private void OnKeyReleased(object? sender, KeyEventArgs args)
        {
            KeyGesture gesture = new KeyGesture(args.Key, KeyAction.Release);

            if (_bindings.TryGetValue(gesture, out IKeyObserver? observer))
            {
                observer.OnKeyReleased(sender, args);
            }
        }
    }
}
