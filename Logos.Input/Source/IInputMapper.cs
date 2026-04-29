using System;

namespace Logos.Input
{
    /// <summary>
    /// Represents an event router that notifies input observers of events triggered by mapped input
    /// gestures.
    /// </summary>
    public interface IInputMapper
    {
        /// <summary>
        /// Gets or sets a value that indicates whether the <see cref="IInputMapper"/> is routing
        /// events to mapped input observers.
        /// </summary>
        /// <param name="value">
        /// Enables event routing if <see langword="true"/>; otherwise, disables event routing.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if event routing is enabled; otherwise, <see langword="false"/>.
        /// </returns>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Occurs when the <see cref="IInputMapper"/> is enabled or disabled.
        /// </summary>
        event EventHandler<bool>? EnabledChanged;
    }
}
