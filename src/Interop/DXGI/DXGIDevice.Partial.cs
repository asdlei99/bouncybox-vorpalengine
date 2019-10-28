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
            DXGI_SURFACE_DESC* pDesc,
            uint NumSurfaces,
            uint Usage,
            [Optional] DXGI_SHARED_RESOURCE* pSharedResource,
            out DXGISurface? surface)
        {
            IDXGISurface* pSurface;
            int hr = Pointer->CreateSurface(pDesc, NumSurfaces, Usage, pSharedResource, &pSurface);

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

        public HResult QueryResourceResidency(ReadOnlySpan<IUnknownPointer> resources, out DXGI_RESIDENCY residencyStatus)
        {
            fixed (IUnknownPointer* pResources = resources)
            fixed (DXGI_RESIDENCY* pResidencyStatus = &residencyStatus)
            {
                return Pointer->QueryResourceResidency((IUnknown**)pResources, pResidencyStatus, (uint)resources.Length);
            }
        }
    }
}