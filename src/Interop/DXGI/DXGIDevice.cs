using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
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
            DXGI_SURFACE_DESC* desc,
            uint numSurfaces,
            uint usage,
            [Optional] DXGI_SHARED_RESOURCE* sharedResource,
            IDXGISurface** surface)
        {
            return Pointer->CreateSurface(desc, numSurfaces, usage, sharedResource, surface);
        }

        public HResult GetAdapter(IDXGIAdapter** adapter)
        {
            return Pointer->GetAdapter(adapter);
        }

        public HResult GetGPUThreadPriority(int* priority)
        {
            return Pointer->GetGPUThreadPriority(priority);
        }

        public HResult QueryResourceResidency(IUnknown** resources, DXGI_RESIDENCY* residencyStatus, uint numResources)
        {
            return Pointer->QueryResourceResidency(resources, residencyStatus, numResources);
        }

        public HResult SetGPUThreadPriority(int priority)
        {
            return Pointer->SetGPUThreadPriority(priority);
        }

        public static implicit operator IDXGIDevice*(DXGIDevice value)
        {
            return value.Pointer;
        }
    }
}