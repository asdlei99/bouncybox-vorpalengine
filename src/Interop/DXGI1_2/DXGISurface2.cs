﻿using System;
using System.Diagnostics.CodeAnalysis;
using BouncyBox.VorpalEngine.Interop.DXGI;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI1_2
{
    /// <summary>Proxies the <see cref="IDXGISurface2" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGISurface2 : DXGISurface1
    {
        /// <summary>Initializes a new instance of the <see cref="DXGISurface2" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGISurface2(IDXGISurface2* pointer) : base((IDXGISurface1*)pointer)
        {
        }

        public new IDXGISurface2* Pointer => (IDXGISurface2*)base.Pointer;

        public HResult GetResource(Guid* iid, void** parentResource, uint* subresourceIndex)
        {
            return Pointer->GetResource(iid, parentResource, subresourceIndex);
        }

        public static implicit operator IDXGISurface2*(DXGISurface2 value)
        {
            return value.Pointer;
        }
    }
}