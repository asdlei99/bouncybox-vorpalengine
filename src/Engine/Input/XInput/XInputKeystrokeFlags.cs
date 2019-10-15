using System;
using BouncyBox.VorpalEngine.Engine.Interop;

namespace BouncyBox.VorpalEngine.Engine.Input.XInput
{
    /// <summary>
    ///     XInput keystroke flags.
    /// </summary>
    [Flags]
    public enum XInputKeystrokeFlags
    {
        /// <summary>
        ///     None.
        /// </summary>
        None = 0,

        /// <summary>
        ///     The key is down.
        /// </summary>
        KeyDown = XInput14.XINPUT_KEYSTROKE_KEYDOWN,

        /// <summary>
        ///     The key is up.
        /// </summary>
        KeyUp = XInput14.XINPUT_KEYSTROKE_KEYUP,

        /// <summary>
        ///     The key is repeating.
        /// </summary>
        Repeat = XInput14.XINPUT_KEYSTROKE_REPEAT
    }
}