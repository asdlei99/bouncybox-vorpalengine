using System;
using System.ComponentModel;
using BouncyBox.VorpalEngine.Engine.Interop;

namespace BouncyBox.VorpalEngine.Engine.Input.XInput
{
    /// <summary>
    ///     An XInput gamepad that wraps access to the underlying XInput 1.4 API.
    /// </summary>
    public class Gamepad
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Gamepad" /> type.
        /// </summary>
        /// <param name="userIndex">A user index.</param>
        public Gamepad(byte userIndex)
        {
            UserIndex = userIndex;
        }

        /// <summary>
        ///     Gets the user index.
        /// </summary>
        public byte UserIndex { get; }

        /// <summary>
        ///     Wraps a call to <see cref="XInput14.XInputGetAudioDeviceIds" />.
        /// </summary>
        /// <exception cref="Win32Exception">Thrown when <see cref="XInput14.XInputGetAudioDeviceIds" /> failed.</exception>
        public unsafe GetAudioDeviceIdsResult GetAudioDeviceIds(ushort* pRenderDeviceId, uint* pRenderCount, ushort* pCaptureDeviceId, uint* pCaptureCount)
        {
            uint result = XInput14.XInputGetAudioDeviceIds(UserIndex, pRenderDeviceId, pRenderCount, pCaptureDeviceId, pCaptureCount);

            return result switch
            {
                Interop.Windows.ERROR_SUCCESS => GetAudioDeviceIdsResult.Success,
                Interop.Windows.ERROR_DEVICE_NOT_CONNECTED => GetAudioDeviceIdsResult.DeviceNotConnected,
                _ => throw new Win32Exception((int)result)
            };
        }

        /// <summary>
        ///     Wraps a call to <see cref="XInput14.XInputGetBatteryInformation" />.
        /// </summary>
        public unsafe XInput14.XINPUT_BATTERY_INFORMATION? GetBatteryInformation(byte devType)
        {
            XInput14.XINPUT_BATTERY_INFORMATION xInputBatteryInformation;

            return XInput14.XInputGetBatteryInformation(UserIndex, devType, &xInputBatteryInformation) == Interop.Windows.ERROR_SUCCESS
                       ? xInputBatteryInformation
                       : (XInput14.XINPUT_BATTERY_INFORMATION?)null;
        }

        /// <summary>
        ///     Wraps a call to <see cref="XInput14.XInputGetCapabilities" />.
        /// </summary>
        /// <exception cref="Win32Exception">Thrown when <see cref="XInput14.XInputGetCapabilities" /> failed.</exception>
        public unsafe (GetCapabilitiesResult result, XInput14.XINPUT_CAPABILITIES? xInputCapabilities) GetCapabilities(uint dwFlags)
        {
            XInput14.XINPUT_CAPABILITIES xInputCapabilities;
            uint result = XInput14.XInputGetCapabilities(UserIndex, dwFlags, &xInputCapabilities);

            return result switch
            {
                Interop.Windows.ERROR_SUCCESS => (GetCapabilitiesResult.Success, xInputCapabilities),
                Interop.Windows.ERROR_DEVICE_NOT_CONNECTED => (GetCapabilitiesResult.DeviceNotConnected, (XInput14.XINPUT_CAPABILITIES?)null),
                _ => throw new Win32Exception((int)result)
            };
        }

        /// <summary>
        ///     Wraps a call to <see cref="XInput14.XInputGetDSoundAudioDeviceGuids" />.
        /// </summary>
        /// <exception cref="Win32Exception">Thrown when <see cref="XInput14.XInputGetDSoundAudioDeviceGuids" /> failed.</exception>
        public unsafe (GetDSoundAudioDeviceGuidsResult result, Guid? dSoundRenderGuid, Guid? dSoundCaptureGuid) GetDSoundAudioDeviceGuids()
        {
            Guid dSoundRenderGuid;
            Guid dSoundCaptureGuid;
            uint result = XInput14.XInputGetDSoundAudioDeviceGuids(UserIndex, &dSoundRenderGuid, &dSoundCaptureGuid);

            return result switch
            {
                Interop.Windows.ERROR_SUCCESS => (GetDSoundAudioDeviceGuidsResult.Success, dSoundRenderGuid, dSoundCaptureGuid),
                Interop.Windows.ERROR_DEVICE_NOT_CONNECTED => (GetDSoundAudioDeviceGuidsResult.DeviceNotConnected, (Guid?)null, (Guid?)null),
                _ => throw new Win32Exception((int)result)
            };
        }

        /// <summary>
        ///     Wraps a call to <see cref="XInput14.XInputGetKeystroke" />.
        /// </summary>
        /// <exception cref="Win32Exception">Thrown when <see cref="XInput14.XInputGetKeystroke" /> failed.</exception>
        public unsafe (GetKeystrokeResult result, XInputKeystroke? keystroke) GetKeystroke()
        {
            XInput14.XINPUT_KEYSTROKE xInputKeystroke;
            uint result = XInput14.XInputGetKeystroke(UserIndex, 0, &xInputKeystroke);

            return result switch
            {
                Interop.Windows.ERROR_SUCCESS => (GetKeystrokeResult.Success, new XInputKeystroke(ref xInputKeystroke)),
                Interop.Windows.ERROR_EMPTY => (GetKeystrokeResult.Empty, (XInputKeystroke?)null),
                Interop.Windows.ERROR_DEVICE_NOT_CONNECTED => (GetKeystrokeResult.DeviceNotConnected, (XInputKeystroke?)null),
                _ => throw new Win32Exception((int)result)
            };
        }

        /// <summary>
        ///     Wraps a call to <see cref="XInput14.XInputGetState" />.
        /// </summary>
        /// <exception cref="Win32Exception">Thrown when <see cref="XInput14.XInputGetState" /> failed.</exception>
        public unsafe (GetStateResult result, XInputState? state) GetState()
        {
            XInput14.XINPUT_STATE xInputState;
            uint result = XInput14.XInputGetState(UserIndex, &xInputState);

            return result switch
            {
                Interop.Windows.ERROR_SUCCESS => (GetStateResult.Success, new XInputState(xInputState)),
                Interop.Windows.ERROR_DEVICE_NOT_CONNECTED => (GetStateResult.DeviceNotConnected, (XInputState?)null),
                _ => throw new Win32Exception((int)result)
            };
        }

        /// <summary>
        ///     Wraps a call to <see cref="XInput14.XInputSetState" />.
        /// </summary>
        /// <exception cref="Win32Exception">Thrown when <see cref="XInput14.XInputSetState" /> failed.</exception>
        public unsafe SetStateResult SetState(XInput14.XINPUT_VIBRATION xInputVibration)
        {
            uint result = XInput14.XInputSetState(UserIndex, &xInputVibration);

            return result switch
            {
                Interop.Windows.ERROR_SUCCESS => SetStateResult.Success,
                Interop.Windows.ERROR_DEVICE_NOT_CONNECTED => SetStateResult.DeviceNotConnected,
                _ => throw new Win32Exception((int)result)
            };
        }

        /// <summary>
        ///     Wraps a call to <see cref="XInput14.XInputEnable" />.
        /// </summary>
        public static void Enable()
        {
            XInput14.XInputEnable(TerraFX.Interop.Windows.TRUE);
        }

        /// <summary>
        ///     Wraps a call to <see cref="XInput14.XInputEnable" />.
        /// </summary>
        public static void Disable()
        {
            XInput14.XInputEnable(TerraFX.Interop.Windows.FALSE);
        }
    }
}