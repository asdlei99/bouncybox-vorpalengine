using System.Runtime.InteropServices;
using BouncyBox.VorpalEngine.Engine.Interop.DXGI;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1_1
{
    public unsafe partial class D2D1Device
    {
        public HResult CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS options, out D2D1DeviceContext? deviceContext)
        {
            ID2D1DeviceContext* pDeviceContext;
            int hr = Pointer->CreateDeviceContext(options, &pDeviceContext);

            deviceContext = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1DeviceContext(pDeviceContext) : null;

            return hr;
        }

        public HResult CreatePrintControl(
            IWICImagingFactory* wicFactory,
            IPrintDocumentPackageTarget* documentTarget,
            [Optional] D2D1_PRINT_CONTROL_PROPERTIES* printControlProperties,
            out D2D1PrintControl? printControl)
        {
            ID2D1PrintControl* pPrintControl;
            int hr = Pointer->CreatePrintControl(wicFactory, documentTarget, printControlProperties, &pPrintControl);

            printControl = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1PrintControl(pPrintControl) : null;

            return hr;
        }

        public static HResult Create(
            DXGIDevice dxgiDevice,
            [Optional] [DefaultParameterValue(false)]
            bool debug,
            out D2D1Device? d2d1Device)
        {
            var d2d1CreationProperties =
                new D2D1_CREATION_PROPERTIES
                {
                    threadingMode = D2D1_THREADING_MODE.D2D1_THREADING_MODE_MULTI_THREADED,
                    debugLevel = debug ? D2D1_DEBUG_LEVEL.D2D1_DEBUG_LEVEL_INFORMATION : D2D1_DEBUG_LEVEL.D2D1_DEBUG_LEVEL_NONE,
                    options = D2D1_DEVICE_CONTEXT_OPTIONS.D2D1_DEVICE_CONTEXT_OPTIONS_ENABLE_MULTITHREADED_OPTIMIZATIONS
                };
            ID2D1Device* pD2D1Device;
            int hr = TerraFX.Interop.D2D1.D2D1CreateDevice(dxgiDevice, &d2d1CreationProperties, &pD2D1Device);

            d2d1Device = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Device(pD2D1Device) : null;

            return hr;
        }
    }
}