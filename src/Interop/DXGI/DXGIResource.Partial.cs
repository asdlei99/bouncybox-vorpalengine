using System;
using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIResource
    {
        public HResult GetEvictionPriority(out uint evictionPriority)
        {
            fixed (uint* pEvictionPriority = &evictionPriority)
            {
                return Pointer->GetEvictionPriority(pEvictionPriority);
            }
        }

        public HResult GetSharedHandle(out IntPtr sharedHandle)
        {
            fixed (IntPtr* pSharedHandle = &sharedHandle)
            {
                return Pointer->GetSharedHandle(pSharedHandle);
            }
        }

        public HResult GetUsage(uint usage)
        {
            return Pointer->GetUsage(&usage);
        }
    }
}