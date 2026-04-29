using System;
using System.Collections.Generic;

namespace Logos.Input
{
    /// <summary>
    /// Represents an event router composed of child input mappers that notify input observers of
    /// events triggered by mapped input gestures.
    /// </summary>
    public class CompositeInputMapper : List<IInputMapper>, IInputMapper
    {
        private bool _isEnabled;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeInputMapper"/> class that is
        /// composed of child input mappers that route events to input observers when enabled.
        /// </summary>
        public CompositeInputMapper()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeInputMapper"/> class that is
        /// composed of child input mappers that route events to input observers when enabled.
        /// </summary>
        /// <param name="isEnabled">
        /// Enables event routing if <see langword="true"/>; otherwise, disables event routing.
        /// </param>
        public CompositeInputMapper(bool isEnabled)
        {
            _isEnabled = isEnabled;
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the <see cref="CompositeInputMapper"/> is
        /// allowing its children to route events to mapped input observers.
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

                foreach (IInputMapper mapper in this)
                {
                    mapper?.IsEnabled = value;
                }

                _isEnabled = value;
                EnabledChanged?.Invoke(this, value);
            }
        }

        /// <summary>
        /// Occurs when the <see cref="CompositeInputMapper"/> is enabled or disabled.
        /// </summary>
        public event EventHandler<bool>? EnabledChanged;
    }
}
