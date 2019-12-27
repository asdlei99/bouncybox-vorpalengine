using System;
using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIDeviceSubObject
    {
        public HResult GetDevice<T>(out T* device)
            where T : unmanaged
        {
            Guid iid = typeof(T).GUID;

            fixed (T** ppDevice = &device)
            {
                return Pointer->GetDevice(&iid, (void**)ppDevice);
            }
        }
    }
}