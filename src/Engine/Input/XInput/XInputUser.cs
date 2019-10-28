using BouncyBox.VorpalEngine.Interop;

namespace BouncyBox.VorpalEngine.Engine.Input.XInput
{
    /// <summary>An XInput user.</summary>
    public enum XInputUser : byte
    {
        /// <summary>User one.</summary>
        One,

        /// <summary>User two.</summary>
        Two,

        /// <summary>User three.</summary>
        Three,

        /// <summary>User four.</summary>
        Four,

        /// <summary>Any user.</summary>
        Any = XInput14.XUSER_INDEX_ANY
    }
}