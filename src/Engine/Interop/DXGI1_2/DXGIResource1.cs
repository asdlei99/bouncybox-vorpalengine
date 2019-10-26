using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using BouncyBox.VorpalEngine.Engine.Interop.DXGI;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGI1_2
{
    /// <summary>Proxies the <see cref="IDXGIResource1" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIResource1 : DXGIResource
    {
        /// <summary>Initializes a new instance of the <see cref="DXGIResource1" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGIResource1(IDXGIResource1* pointer) : base((IDXGIResource*)pointer)
        {
        }

        public new IDXGIResource1* Pointer => (IDXGIResource1*)base.Pointer;

        public HResult CreateSharedHandle([Optional] SECURITY_ATTRIBUTES* pAttributes, uint dwAccess, [Optional] ReadOnlySpan<char> name, IntPtr* pHandle)
        {
            fixed (char* pName = name)
            {
                return Pointer->CreateSharedHandle(pAttributes, dwAccess, (ushort*)pName, pHandle);
            }
        }

        public HResult CreateSubresourceSurface(uint index, IDXGISurface2** ppSurface)
        {
            return Pointer->CreateSubresourceSurface(index, ppSurface);
        }

        public static implicit operator IDXGIResource1*(DXGIResource1 value)
        {
            return value.Pointer;
        }
    }
}