using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class Windows
    {
        public const int ERROR_DEVICE_NOT_CONNECTED = 1167;
        public const int ERROR_EMPTY = 4306;
        public const int ERROR_SUCCESS = 0;
        public const int S_OK = 0;
        public const uint E_NOINTERFACE = 0x80004002;
        public const uint E_POINTER = 0x80004003;
    }
}