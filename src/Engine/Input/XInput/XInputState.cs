using BouncyBox.VorpalEngine.Engine.Interop;

namespace BouncyBox.VorpalEngine.Engine.Input.XInput
{
    /// <summary>The state of an XInput gamepad.</summary>
    public struct XInputState
    {
        /// <summary>Initializes a new instance of the <see cref="XInputState" /> type.</summary>
        /// <param name="xInputState">The <see cref="XInput14.XINPUT_STATE" /> to map.</param>
        public XInputState(XInput14.XINPUT_STATE xInputState)
        {
            PacketNumber = xInputState.dwPacketNumber;
            Buttons = (XInputGamepadButton)xInputState.Gamepad.wButtons;
            LeftTrigger = xInputState.Gamepad.bLeftTrigger;
            RightTrigger = xInputState.Gamepad.bRightTrigger;
            LeftThumbX = xInputState.Gamepad.sThumbLX;
            LeftThumbY = xInputState.Gamepad.sThumbLY;
            RightThumbX = xInputState.Gamepad.sThumbRX;
            RightThumbY = xInputState.Gamepad.sThumbRY;
        }

        /// <summary>Gets the packet number.</summary>
        public uint PacketNumber { get; }

        /// <summary>Gets the down buttons.</summary>
        public XInputGamepadButton Buttons { get; }

        /// <summary>Gets the left trigger value.</summary>
        public byte LeftTrigger { get; }

        /// <summary>Gets the right trigger value.</summary>
        public byte RightTrigger { get; }

        /// <summary>Gets the left thumb X-axis value.</summary>
        public short LeftThumbX { get; }

        /// <summary>Gets the left thumb Y-axis value.</summary>
        public short LeftThumbY { get; }

        /// <summary>Gets the right thumb X-axis value.</summary>
        public short RightThumbX { get; }

        /// <summary>Gets the right thumb Y-axis value.</summary>
        public short RightThumbY { get; }
    }
}