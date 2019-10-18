using BouncyBox.VorpalEngine.Engine.Interop;

namespace BouncyBox.VorpalEngine.Engine.Input.XInput
{
    /// <summary>An XInput keystroke.</summary>
    public struct XInputKeystroke
    {
        /// <summary>Gets the XInput virtual key.</summary>
        public XInputVirtualKey VirtualKey { get; }

        /// <summary>Gets the XInput keystroke flags.</summary>
        public XInputKeystrokeFlags Flags { get; }

        /// <summary>Gets the UNICODE representation of the virtual key.</summary>
        public char Unicode { get; }

        /// <summary>Initializes a new instance of the <see cref="XInputKeystroke" /> type.</summary>
        /// <param name="xInputKeystroke">The <see cref="XInput14.XINPUT_KEYSTROKE" /> to map.</param>
        public XInputKeystroke(ref XInput14.XINPUT_KEYSTROKE xInputKeystroke)
        {
            VirtualKey = (XInputVirtualKey)xInputKeystroke.VirtualKey;
            Flags = (XInputKeystrokeFlags)xInputKeystroke.Flags;
            Unicode = (char)xInputKeystroke.Unicode;
        }
    }
}