using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1CommandSink4" /> COM interface.</summary>
    public unsafe class D2D1CommandSink4 : D2D1CommandSink3
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1CommandSink4" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1CommandSink4(ID2D1CommandSink4* pointer) : base((ID2D1CommandSink3*)pointer)
        {
        }

        public new ID2D1CommandSink4* Pointer => (ID2D1CommandSink4*)base.Pointer;

        public HResult SetPrimitiveBlend2(D2D1_PRIMITIVE_BLEND primitiveBlend)
        {
            return Pointer->SetPrimitiveBlend2(primitiveBlend);
        }

        public static implicit operator ID2D1CommandSink4*(D2D1CommandSink4 value)
        {
            return value.Pointer;
        }
    }
}