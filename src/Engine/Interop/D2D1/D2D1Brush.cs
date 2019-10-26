using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1Brush" /> COM interface.</summary>
    public unsafe partial class D2D1Brush : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Brush" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Brush(ID2D1Brush* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1Brush* Pointer => (ID2D1Brush*)base.Pointer;

        public float GetOpacity()
        {
            return Pointer->GetOpacity();
        }

        public void GetTransform(D2D_MATRIX_3X2_F* transform)
        {
            Pointer->GetTransform(transform);
        }

        public void SetOpacity(float opacity)
        {
            Pointer->SetOpacity(opacity);
        }

        public void SetTransform(D2D_MATRIX_3X2_F* transform)
        {
            Pointer->SetTransform(transform);
        }

        public static implicit operator ID2D1Brush*(D2D1Brush value)
        {
            return value.Pointer;
        }
    }
}