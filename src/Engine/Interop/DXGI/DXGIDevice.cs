using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGI
{
    /// <summary>Proxies the <see cref="IDXGIDevice" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIDevice : DXGIObject
    {
        /// <summary>Initializes a new instance of the <see cref="DXGIDevice" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGIDevice(IDXGIDevice* pointer) : base((IDXGIObject*)pointer)
        {
        }

        public new IDXGIDevice* Pointer => (IDXGIDevice*)base.Pointer;

        public HResult CreateSurface(
            DXGI_SURFACE_DESC* pDesc,
            uint NumSurfaces,
            uint Usage,
            [Optional] DXGI_SHARED_RESOURCE* pSharedResource,
            IDXGISurface** ppSurface)
        {
            return Pointer->CreateSurface(pDesc, NumSurfaces, Usage, pSharedResource, ppSurface);
        }

        public HResult GetAdapter(IDXGIAdapter** pAdapter)
        {
            return Pointer->GetAdapter(pAdapter);
        }

        public HResult GetGPUThreadPriority(int* pPriority)
        {
            return Pointer->GetGPUThreadPriority(pPriority);
        }

        public HResult QueryResourceResidency(IUnknown** ppResources, DXGI_RESIDENCY* pResidencyStatus, uint NumResources)
        {
            return Pointer->QueryResourceResidency(ppResources, pResidencyStatus, NumResources);
        }

        public HResult SetGPUThreadPriority(int Priority)
        {
            return Pointer->SetGPUThreadPriority(Priority);
        }

        public static implicit operator IDXGIDevice*(DXGIDevice value)
        {
            return value.Pointer;
        }
    }
}