using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIDevice
    {
        public HResult CreateSurface(
            DXGI_SURFACE_DESC* desc,
            uint numSurfaces,
            uint usage,
            [Optional] DXGI_SHARED_RESOURCE* sharedResource,
            out DXGISurface? surface)
        {
            IDXGISurface* pSurface;
            int hr = Pointer->CreateSurface(desc, numSurfaces, usage, sharedResource, &pSurface);

            surface = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DXGISurface(pSurface) : null;

            return hr;
        }

        public HResult GetAdapter(out DXGIAdapter? adapter)
        {
            IDXGIAdapter* pAdapter;
            int hr = Pointer->GetAdapter(&pAdapter);

            adapter = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DXGIAdapter(pAdapter) : null;

            return hr;
        }

        public HResult GetGPUThreadPriority(out int priority)
        {
            fixed (int* pPriority = &priority)
            {
                return Pointer->GetGPUThreadPriority(pPriority);
            }
        }

        public HResult QueryResourceResidency(ReadOnlySpan<Pointer<IUnknown>> resources, out DXGI_RESIDENCY residencyStatus)
        {
            fixed (Pointer<IUnknown>* pResources = resources)
            fixed (DXGI_RESIDENCY* pResidencyStatus = &residencyStatus)
            {
                return Pointer->QueryResourceResidency((IUnknown**)pResources, pResidencyStatus, (uint)resources.Length);
            }
        }
    }
}