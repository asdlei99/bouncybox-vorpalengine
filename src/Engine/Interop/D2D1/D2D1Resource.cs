using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1Resource" /> COM interface.</summary>
    public unsafe partial class D2D1Resource : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Resource" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Resource(ID2D1Resource* pointer) : base((IUnknown*)pointer)
        {
        }

        public new ID2D1Resource* Pointer => (ID2D1Resource*)base.Pointer;

        public void GetFactory(ID2D1Factory** factory)
        {
            Pointer->GetFactory(factory);
        }

        public static implicit operator ID2D1Resource*(D2D1Resource value)
        {
            return value.Pointer;
        }
    }
}