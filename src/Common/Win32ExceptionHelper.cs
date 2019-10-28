using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace BouncyBox.VorpalEngine.Common
{
    /// <summary>Helper methods for generating Win32 exceptions.</summary>
    public static class Win32ExceptionHelper
    {
        /// <summary>Gets an appropriately-typed exception for the last Win32 error.</summary>
        /// <returns>Returns an exception.</returns>
        public static Exception GetException()
        {
            int errorCode = Marshal.GetHRForLastWin32Error();

            return Marshal.GetExceptionForHR(errorCode) ?? new Win32Exception(errorCode);
        }
    }
}