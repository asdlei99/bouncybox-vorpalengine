using BouncyBox.VorpalEngine.Engine.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Input.XInput
{
    /// <summary>Wraps the result of a call to <see cref="XInput14.XInputGetState" />.</summary>
    public enum GetStateResult
    {
        Success,
        DeviceNotConnected
    }
}