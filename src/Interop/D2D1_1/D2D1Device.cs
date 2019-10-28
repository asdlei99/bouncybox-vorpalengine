using System.Runtime.InteropServices;
using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_1
{
    /// <summary>Proxies the <see cref="ID2D1Device" /> COM interface.</summary>
    public unsafe partial class D2D1Device : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Device" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Device(ID2D1Device* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1Device* Pointer => (ID2D1Device*)base.Pointer;

        public void ClearResources(uint millisecondsSinceUse)
        {
            Pointer->ClearResources(millisecondsSinceUse);
        }

        public HResult CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS options, ID2D1DeviceContext** deviceContext)
        {
            return Pointer->CreateDeviceContext(options, deviceContext);
        }

        public HResult CreatePrintControl(
            IWICImagingFactory* wicFactory,
            IPrintDocumentPackageTarget* documentTarget,
            [Optional] D2D1_PRINT_CONTROL_PROPERTIES* printControlProperties,
            ID2D1PrintControl** printControl)
        {
            return Pointer->CreatePrintControl(wicFactory, documentTarget, printControlProperties, printControl);
        }

        public ulong GetMaximumTextureMemory()
        {
            return Pointer->GetMaximumTextureMemory();
        }

        public void SetMaximumTextureMemory(ulong maximumInBytes)
        {
            Pointer->SetMaximumTextureMemory(maximumInBytes);
        }

        public static implicit operator ID2D1Device*(D2D1Device value)
        {
            return value.Pointer;
        }
    }
}