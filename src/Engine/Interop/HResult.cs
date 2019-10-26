using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop
{
    /// <summary>An HRESULT.</summary>
    public readonly struct HResult
    {
        /// <summary>Initializes a new instance of the <see cref="HResult" /> type.</summary>
        /// <param name="hr">The HRESULT.</param>
        public HResult(int hr)
        {
            HR = hr;
            Succeeded = TerraFX.Interop.Windows.SUCCEEDED(HR);
            Failed = TerraFX.Interop.Windows.FAILED(HR);
        }

        /// <summary>Gets the HRESULT.</summary>
        // ReSharper disable once InconsistentNaming
        public int HR { get; }

        /// <summary>Gets a value indicating if the HRESULT represents a successful operation.</summary>
        public bool Succeeded { get; }

        /// <summary>Gets a value indicating if the HRESULT represents a failed operation.</summary>
        public bool Failed { get; }

        /// <summary>Throws an exception if the HRESULT represents a failed operation and the HRESULT is not an allowed failure.</summary>
        /// <param name="exceptionMessage">An exception message.</param>
        /// <param name="allowedFailureHRs">Allowed failure HRESULTs.</param>
        [DebuggerStepThrough]
        public void ThrowIfFailed(string exceptionMessage = "Operation failed.", params int[] allowedFailureHRs)
        {
            if (Succeeded || Array.IndexOf(allowedFailureHRs, HR) > -1)
            {
                return;
            }

            throw new ComObjectException(exceptionMessage, Marshal.GetExceptionForHR(HR) ?? new Win32Exception(HR));
        }

        public static implicit operator HResult(int hr)
        {
            return new HResult(hr);
        }

        public static implicit operator HResult(uint hr)
        {
            return new HResult(unchecked((int)hr));
        }

        public static implicit operator int(HResult hResult)
        {
            return hResult.HR;
        }
    }
}