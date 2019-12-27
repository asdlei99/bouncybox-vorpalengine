using BouncyBox.VorpalEngine.Interop.D2D1_1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_2
{
    /// <summary>Proxies the <see cref="ID2D1CommandSink1" /> COM interface.</summary>
    public unsafe class D2D1CommandSink1 : D2D1CommandSink
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1CommandSink1" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1CommandSink1(ID2D1CommandSink1* pointer) : base((ID2D1CommandSink*)pointer)
        {
        }

        public new ID2D1CommandSink1* Pointer => (ID2D1CommandSink1*)base.Pointer;

        public HResult SetPrimitiveBlend1(D2D1_PRIMITIVE_BLEND primitiveBlend)
        {
            return Pointer->SetPrimitiveBlend1(primitiveBlend);
        }

        public static implicit operator ID2D1CommandSink1*(D2D1CommandSink1 value)
        {
            return value.Pointer;
        }
    }
}