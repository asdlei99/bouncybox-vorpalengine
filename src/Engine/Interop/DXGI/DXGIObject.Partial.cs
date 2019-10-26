using System;
using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGI
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIObject
    {
        public HResult GetParent<T>(out T* pParent)
            where T : unmanaged
        {
            Guid riid = typeof(T).GUID;

            fixed (T** ppParent = &pParent)
            {
                return Pointer->GetParent(&riid, (void**)ppParent);
            }
        }

        public HResult GetPrivateData(Guid* Name, ref Span<byte> data)
        {
            var uiDataSize = (uint)data.Length;
            int hr;

            fixed (byte* pData = data)
            {
                hr = Pointer->GetPrivateData(Name, &uiDataSize, pData);
            }

            data = data.Slice(0, (int)uiDataSize);

            return hr;
        }

        public HResult SetPrivateData(Guid* Name, Span<byte> data)
        {
            fixed (byte* pData = data)
            {
                return Pointer->SetPrivateData(Name, (uint)data.Length, pData);
            }
        }
    }
}