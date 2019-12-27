using System;
using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIObject
    {
        public HResult GetParent<T>(out T* parent)
            where T : unmanaged
        {
            Guid riid = typeof(T).GUID;

            fixed (T** ppParent = &parent)
            {
                return Pointer->GetParent(&riid, (void**)ppParent);
            }
        }

        public HResult GetPrivateData(Guid* name, ref Span<byte> data)
        {
            var uiDataSize = (uint)data.Length;
            int hr;

            fixed (byte* pData = data)
            {
                hr = Pointer->GetPrivateData(name, &uiDataSize, pData);
            }

            data = data.Slice(0, (int)uiDataSize);

            return hr;
        }

        public HResult SetPrivateData(Guid* name, Span<byte> data)
        {
            fixed (byte* pData = data)
            {
                return Pointer->SetPrivateData(name, (uint)data.Length, pData);
            }
        }
    }
}