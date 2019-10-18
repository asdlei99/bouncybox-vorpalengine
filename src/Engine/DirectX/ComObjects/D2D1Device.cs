using BouncyBox.VorpalEngine.Engine.Interop;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>Proxies the <see cref="ID2D1Device" /> COM interface.</summary>
    public unsafe class D2D1Device : ComObject<ID2D1Device>
    {
        private ComPtr<ID2D1Device> _d2d1Device;

        /// <inheritdoc />
        /// <summary>Initializes a new instance of the <see cref="D2D1Device" /> type by proxying <see cref="D2D1.D2D1CreateDevice" />.</summary>
        public D2D1Device(DXGIDevice dxgiDevice, bool debug = false)
        {
            var d2d1CreationProperties =
                new D2D1_CREATION_PROPERTIES
                {
                    threadingMode = D2D1_THREADING_MODE.D2D1_THREADING_MODE_MULTI_THREADED,
                    debugLevel = debug ? D2D1_DEBUG_LEVEL.D2D1_DEBUG_LEVEL_INFORMATION : D2D1_DEBUG_LEVEL.D2D1_DEBUG_LEVEL_NONE,
                    options = D2D1_DEVICE_CONTEXT_OPTIONS.D2D1_DEVICE_CONTEXT_OPTIONS_ENABLE_MULTITHREADED_OPTIMIZATIONS
                };

            // ReSharper disable once UnusedVariable
            fixed (ComPtr<ID2D1Device>* a = &_d2d1Device)
            {
                CheckResultHandle(
                    D2D1.D2D1CreateDevice(dxgiDevice, &d2d1CreationProperties, _d2d1Device.GetAddressOf()),
                    $"Failed to create {nameof(ID2D1Device)}.");
            }
        }

        /// <inheritdoc />
        public override ID2D1Device* Pointer => _d2d1Device;

        /// <summary>Proxies <see cref="ID2D1Device.CreateDeviceContext" />.</summary>
        public D2D1DeviceContext CreateDeviceContext()
        {
            var d2d1DeviceContext = new ComPtr<ID2D1DeviceContext>();

            CheckResultHandle(
                Pointer->CreateDeviceContext(
                    D2D1_DEVICE_CONTEXT_OPTIONS.D2D1_DEVICE_CONTEXT_OPTIONS_ENABLE_MULTITHREADED_OPTIMIZATIONS,
                    d2d1DeviceContext.GetAddressOf()),
                $"Failed to create {nameof(ID2D1DeviceContext)}.");

            return new D2D1DeviceContext(d2d1DeviceContext);
        }

        /// <summary>Proxies <see cref="ID2D1Device.GetFactory" />.</summary>
        public D2D1Factory GetFactory()
        {
            var d2d1Factory = new ComPtr<ID2D1Factory>();

            Pointer->GetFactory(d2d1Factory.GetAddressOf());

            return new D2D1Factory(d2d1Factory);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _d2d1Device.Dispose();

            base.Dispose(disposing);
        }
    }
}