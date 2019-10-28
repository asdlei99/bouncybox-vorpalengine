using BouncyBox.VorpalEngine.Interop;

namespace BouncyBox.VorpalEngine.Engine.Input.XInput
{
    /// <summary>An XInput virtual key.</summary>
    public enum XInputVirtualKey
    {
        /// <summary>The A button, usually the bottom of the group of four right-side buttons.</summary>
        A = XInput14.VK_PAD_A,

        /// <summary>The B button, usually the right of the group of four right-side buttons.</summary>
        B = XInput14.VK_PAD_B,

        /// <summary>The X button, usually the left of the group of four right-side buttons.</summary>
        X = XInput14.VK_PAD_X,

        /// <summary>The Y button, usually the top of the group of four right-side buttons.</summary>
        Y = XInput14.VK_PAD_Y,

        /// <summary>The right shoulder button.</summary>
        RightShoulder = XInput14.VK_PAD_RSHOULDER,

        /// <summary>The left shoulder button.</summary>
        LeftShoulder = XInput14.VK_PAD_LSHOULDER,

        /// <summary>The left trigger button.</summary>
        LeftTrigger = XInput14.VK_PAD_LTRIGGER,

        /// <summary>The right trigger button.</summary>
        RightTrigger = XInput14.VK_PAD_RTRIGGER,

        /// <summary>The directional pad up button. Sometimes activated by a thumb stick.</summary>
        DPadUp = XInput14.VK_PAD_DPAD_UP,

        /// <summary>The directional pad down button. Sometimes activated by a thumb stick.</summary>
        DPadDown = XInput14.VK_PAD_DPAD_DOWN,

        /// <summary>The directional pad left button. Sometimes activated by a thumb stick.</summary>
        DPadLeft = XInput14.VK_PAD_DPAD_LEFT,

        /// <summary>The directional pad right button. Sometimes activated by a thumb stick.</summary>
        DPadRight = XInput14.VK_PAD_DPAD_RIGHT,

        /// <summary>Start. Sometimes called Options.</summary>
        Start = XInput14.VK_PAD_START,

        /// <summary>Back. Sometimes called Select.</summary>
        Back = XInput14.VK_PAD_BACK,

        /// <summary>The left thumb stick button when it is pressed.</summary>
        LeftThumbPress = XInput14.VK_PAD_LTHUMB_PRESS,

        /// <summary>The right thumb stick button when it is pressed.</summary>
        RightThumbPress = XInput14.VK_PAD_RTHUMB_PRESS,

        /// <summary>The left thumb stick when it it moved up.</summary>
        LeftThumbUp = XInput14.VK_PAD_LTHUMB_UP,

        /// <summary>The left thumb stick when it it moved down.</summary>
        LeftThumbDown = XInput14.VK_PAD_LTHUMB_DOWN,

        /// <summary>The left thumb stick when it it moved right.</summary>
        LeftThumbRight = XInput14.VK_PAD_LTHUMB_RIGHT,

        /// <summary>The left thumb stick when it it moved left.</summary>
        LeftThumbLeft = XInput14.VK_PAD_LTHUMB_LEFT,

        /// <summary>The left thumb stick when it it moved up and left.</summary>
        LeftThumbUpLeft = XInput14.VK_PAD_LTHUMB_UPLEFT,

        /// <summary>The left thumb stick when it it moved up and right.</summary>
        LeftThumbUpRight = XInput14.VK_PAD_LTHUMB_UPRIGHT,

        /// <summary>The left thumb stick when it it moved down and right.</summary>
        LeftThumbDownRight = XInput14.VK_PAD_LTHUMB_DOWNRIGHT,

        /// <summary>The left thumb stick when it it moved down and left.</summary>
        LeftThumbDownLeft = XInput14.VK_PAD_LTHUMB_DOWNLEFT,

        /// <summary>The right thumb stick when it it moved up.</summary>
        RightThumbUp = XInput14.VK_PAD_RTHUMB_UP,

        /// <summary>The right thumb stick when it it moved down.</summary>
        RightThumbDown = XInput14.VK_PAD_RTHUMB_DOWN,

        /// <summary>The right thumb stick when it it moved right.</summary>
        RightThumbRight = XInput14.VK_PAD_RTHUMB_RIGHT,

        /// <summary>The right thumb stick when it it moved left.</summary>
        RightThumbLeft = XInput14.VK_PAD_RTHUMB_LEFT,

        /// <summary>The right thumb stick when it it moved up and left.</summary>
        RightThumbUpLeft = XInput14.VK_PAD_RTHUMB_UPLEFT,

        /// <summary>The right thumb stick when it it moved up and right.</summary>
        RightThumbUpRight = XInput14.VK_PAD_RTHUMB_UPRIGHT,

        /// <summary>The right thumb stick when it it moved down and right.</summary>
        RightThumbDownRight = XInput14.VK_PAD_RTHUMB_DOWNRIGHT,

        /// <summary>The right thumb stick when it it moved down and left.</summary>
        RightThumbDownLeft = XInput14.VK_PAD_RTHUMB_DOWNLEFT
    }
}