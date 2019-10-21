using System;

namespace BouncyBox.VorpalEngine.Engine.Bootstrap
{
    /// <summary>Logical Windows versions mapped to their actual version numbers.</summary>
    /// <remarks>See <a href="https://www.lifewire.com/windows-version-numbers-2625171">this page</a>.</remarks>
    public static class WindowsVersion
    {
        /// <summary>The minimum Windows version supported by the engine.</summary>
        public static readonly Version MinimumVersion;

        /// <summary>Platform Update for Windows 7.</summary>
        public static readonly Version PlatformUpdateForWindows7 = new Version(6, 1, 7601, 0);

        /// <summary>Windows 10 1803.</summary>
        public static readonly Version Windows10Version1803 = new Version(10, 0, 18362, 0);

        static WindowsVersion()
        {
            MinimumVersion = PlatformUpdateForWindows7;
        }
    }
}