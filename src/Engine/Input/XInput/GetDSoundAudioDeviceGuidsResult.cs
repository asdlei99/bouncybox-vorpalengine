﻿using BouncyBox.VorpalEngine.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Input.XInput
{
    /// <summary>Wraps the result of a call to <see cref="XInput14.XInputGetDSoundAudioDeviceGuids" />.</summary>
    public enum GetDSoundAudioDeviceGuidsResult
    {
        Success,
        DeviceNotConnected
    }
}