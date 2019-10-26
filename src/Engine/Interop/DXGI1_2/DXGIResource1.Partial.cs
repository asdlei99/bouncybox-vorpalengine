using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGI1_2
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIResource1
    {
        public HResult CreateSharedHandle([Optional] SECURITY_ATTRIBUTES* pAttributes, uint dwAccess, [Optional] ReadOnlySpan<char> name, out IntPtr handle)
        {
            fixed (char* pName = name)
            fixed (IntPtr* pHandle = &handle)
            {
                return Pointer->CreateSharedHandle(pAttributes, dwAccess, (ushort*)pName, pHandle);
            }
        }

        public HResult CreateSubresourceSurface(uint index, out DXGISurface2? surface)
        {
            IDXGISurface2* pSurface;
            int hr = Pointer->CreateSubresourceSurface(index, &pSurface);

            surface = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DXGISurface2(pSurface) : null;

            return hr;
        }
    }
}