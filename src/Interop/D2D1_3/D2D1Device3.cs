using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1Device3" /> COM interface.</summary>
    public unsafe partial class D2D1Device3 : D2D1Device2
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Device3" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Device3(ID2D1Device3* pointer) : base((ID2D1Device2*)pointer)
        {
        }

        public new ID2D1Device3* Pointer => (ID2D1Device3*)base.Pointer;

        public HResult CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS options, ID2D1DeviceContext3** deviceContext3)
        {
            return Pointer->CreateDeviceContext(options, deviceContext3);
        }

        public static implicit operator ID2D1Device3*(D2D1Device3 value)
        {
            return value.Pointer;
        }
    }
}