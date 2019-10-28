using System;
using BouncyBox.VorpalEngine.Interop;

namespace BouncyBox.VorpalEngine.Engine.Input.XInput
{
    /// <summary>XInput gamepad buttons.</summary>
    [Flags]
    public enum XInputGamepadButton
    {
        /// <summary>The directional pad up button. Sometimes activated by a thumb stick.</summary>
        DPadUp = XInput14.XINPUT_GAMEPAD_DPAD_UP,

        /// <summary>The directional pad down button. Sometimes activated by a thumb stick.</summary>
        DPadDown = XInput14.XINPUT_GAMEPAD_DPAD_DOWN,

        /// <summary>The directional pad left button. Sometimes activated by a thumb stick.</summary>
        DPadLeft = XInput14.XINPUT_GAMEPAD_DPAD_LEFT,

        /// <summary>The directional pad right button. Sometimes activated by a thumb stick.</summary>
        DPadRight = XInput14.XINPUT_GAMEPAD_DPAD_RIGHT,

        /// <summary>Start. Sometimes called Options.</summary>
        Start = XInput14.XINPUT_GAMEPAD_START,

        /// <summary>Back. Sometimes called Select.</summary>
        Back = XInput14.XINPUT_GAMEPAD_BACK,

        /// <summary>The left thumb stick button.</summary>
        LeftThumb = XInput14.XINPUT_GAMEPAD_LEFT_THUMB,

        /// <summary>The right thumb stick button.</summary>
        RightThumb = XInput14.XINPUT_GAMEPAD_RIGHT_THUMB,

        /// <summary>The left shoulder button.</summary>
        LeftShoulder = XInput14.XINPUT_GAMEPAD_LEFT_SHOULDER,

        /// <summary>The right shoulder button.</summary>
        RightShoulder = XInput14.XINPUT_GAMEPAD_RIGHT_SHOULDER,

        /// <summary>The A button, usually the bottom of the group of four right-side buttons.</summary>
        A = XInput14.XINPUT_GAMEPAD_A,

        /// <summary>The B button, usually the right of the group of four right-side buttons.</summary>
        B = XInput14.XINPUT_GAMEPAD_B,

        /// <summary>The X button, usually the left of the group of four right-side buttons.</summary>
        X = XInput14.XINPUT_GAMEPAD_X,

        /// <summary>The Y button, usually the top of the group of four right-side buttons.</summary>
        Y = XInput14.XINPUT_GAMEPAD_Y
    }
}