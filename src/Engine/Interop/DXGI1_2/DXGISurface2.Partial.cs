using System;
using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGI1_2
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGISurface2
    {
        public HResult GetResource<T>(out T* pParentResource, out uint subresourceIndex)
            where T : unmanaged
        {
            Guid iid = typeof(T).GUID;

            fixed (T** ppParentResource = &pParentResource)
            fixed (uint* pSubresourceIndex = &subresourceIndex)
            {
                return Pointer->GetResource(&iid, (void**)ppParentResource, pSubresourceIndex);
            }
        }
    }
}