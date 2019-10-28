using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1StrokeStyle" /> COM interface.</summary>
    public unsafe partial class D2D1StrokeStyle : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1StrokeStyle" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1StrokeStyle(ID2D1StrokeStyle* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1StrokeStyle* Pointer => (ID2D1StrokeStyle*)base.Pointer;

        public D2D1_CAP_STYLE GetDashCap()
        {
            return Pointer->GetDashCap();
        }

        public void GetDashes(float* dashes, uint dashesCount)
        {
            Pointer->GetDashes(dashes, dashesCount);
        }

        public uint GetDashesCount()
        {
            return Pointer->GetDashesCount();
        }

        public float GetDashOffset()
        {
            return Pointer->GetDashOffset();
        }

        public D2D1_DASH_STYLE GetDashStyle()
        {
            return Pointer->GetDashStyle();
        }

        public D2D1_CAP_STYLE GetEndCap()
        {
            return Pointer->GetEndCap();
        }

        public D2D1_LINE_JOIN GetLineJoin()
        {
            return Pointer->GetLineJoin();
        }

        public float GetMiterLimit()
        {
            return Pointer->GetMiterLimit();
        }

        public D2D1_CAP_STYLE GetStartCap()
        {
            return Pointer->GetStartCap();
        }

        public static implicit operator ID2D1StrokeStyle*(D2D1StrokeStyle value)
        {
            return value.Pointer;
        }
    }
}