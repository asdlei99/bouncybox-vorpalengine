using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1InkStyle" /> COM interface.</summary>
    public unsafe partial class D2D1InkStyle : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1InkStyle" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1InkStyle(ID2D1InkStyle* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1InkStyle* Pointer => (ID2D1InkStyle*)base.Pointer;

        public D2D1_INK_NIB_SHAPE GetNibShape()
        {
            return Pointer->GetNibShape();
        }

        public void GetNibTransform(D2D_MATRIX_3X2_F* transform)
        {
            Pointer->GetNibTransform(transform);
        }

        public void SetNibShape(D2D1_INK_NIB_SHAPE nibShape)
        {
            Pointer->SetNibShape(nibShape);
        }

        public void SetNibTransform(D2D_MATRIX_3X2_F* transform)
        {
            Pointer->SetNibTransform(transform);
        }

        public static implicit operator ID2D1InkStyle*(D2D1InkStyle value)
        {
            return value.Pointer;
        }
    }
}