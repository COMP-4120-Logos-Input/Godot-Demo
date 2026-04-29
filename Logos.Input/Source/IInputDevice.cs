namespace Logos.Input
{
    /// <summary>
    /// Defines methods that report the connection status of an input device.
    /// </summary>
    public interface IInputDevice
    {
        /// <summary>
        /// Returns a value that indicates whether the <see cref="IInputDevice"/> is connected.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the <see cref="IInputDevice"/> is connected; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        bool IsConnected { get; }
    }
}
