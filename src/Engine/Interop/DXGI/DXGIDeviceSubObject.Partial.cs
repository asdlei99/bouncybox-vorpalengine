using System;
using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGI
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIDeviceSubObject
    {
        public HResult GetDevice<T>(out T* pDevice)
            where T : unmanaged
        {
            Guid iid = typeof(T).GUID;

            fixed (T** ppDevice = &pDevice)
            {
                return Pointer->GetDevice(&iid, (void**)ppDevice);
            }
        }
    }
}