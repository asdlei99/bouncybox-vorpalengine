using BouncyBox.VorpalEngine.Engine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1_1
{
    /// <summary>Proxies the <see cref="ID2D1StrokeStyle1" /> COM interface.</summary>
    public unsafe class D2D1StrokeStyle1 : D2D1StrokeStyle
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1StrokeStyle1" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1StrokeStyle1(ID2D1StrokeStyle1* pointer) : base((ID2D1StrokeStyle*)pointer)
        {
        }

        public new ID2D1StrokeStyle1* Pointer => (ID2D1StrokeStyle1*)base.Pointer;

        public D2D1_STROKE_TRANSFORM_TYPE GetStrokeTransformType()
        {
            return Pointer->GetStrokeTransformType();
        }

        public static implicit operator ID2D1StrokeStyle1*(D2D1StrokeStyle1 value)
        {
            return value.Pointer;
        }
    }
}