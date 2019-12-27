using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1Device6" /> COM interface.</summary>
    public unsafe partial class D2D1Device6 : D2D1Device5
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Device6" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Device6(ID2D1Device6* pointer) : base((ID2D1Device5*)pointer)
        {
        }

        public new ID2D1Device6* Pointer => (ID2D1Device6*)base.Pointer;

        public HResult CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS options, ID2D1DeviceContext6** deviceContext6)
        {
            return Pointer->CreateDeviceContext(options, deviceContext6);
        }

        public static implicit operator ID2D1Device6*(D2D1Device6 value)
        {
            return value.Pointer;
        }
    }
}