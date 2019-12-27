using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1Device5" /> COM interface.</summary>
    public unsafe partial class D2D1Device5 : D2D1Device4
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Device5" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Device5(ID2D1Device5* pointer) : base((ID2D1Device4*)pointer)
        {
        }

        public new ID2D1Device5* Pointer => (ID2D1Device5*)base.Pointer;

        public HResult CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS options, ID2D1DeviceContext5** deviceContext5)
        {
            return Pointer->CreateDeviceContext(options, deviceContext5);
        }

        public static implicit operator ID2D1Device5*(D2D1Device5 value)
        {
            return value.Pointer;
        }
    }
}