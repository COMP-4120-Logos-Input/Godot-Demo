using System;
using System.Collections.Generic;
using Logos.Input;

namespace TestProject;

public sealed class GodotKeyboardListener : IKeyboardListener
{
    public IEnumerable<IKeyboardDevice> Devices
    {
        get { yield break; }
    }

    IEnumerable<IInputDevice> IInputListener.Devices
    {
        get { yield break; }
    }

    public event EventHandler<InputEventArgs>? DeviceConnected;
    public event EventHandler<InputEventArgs>? DeviceDisconnected;
    public event EventHandler<KeyEventArgs>? KeyPressed;
    public event EventHandler<KeyEventArgs>? KeyRepeated;
    public event EventHandler<KeyEventArgs>? KeyReleased;

    public void Press(KeyCode key)
    {
        KeyPressed?.Invoke(this, new KeyEventArgs(null!, TimeSpan.Zero, key));
    }

    public void Repeat(KeyCode key)
    {
        KeyRepeated?.Invoke(this, new KeyEventArgs(null!, TimeSpan.Zero, key));
    }

    public void Release(KeyCode key)
    {
        KeyReleased?.Invoke(this, new KeyEventArgs(null!, TimeSpan.Zero, key));
    }
}