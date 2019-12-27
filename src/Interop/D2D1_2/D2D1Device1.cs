using BouncyBox.VorpalEngine.Interop.D2D1_1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_2
{
    /// <summary>Proxies the <see cref="ID2D1Device1" /> COM interface.</summary>
    public unsafe class D2D1Device1 : D2D1Device
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Device1" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Device1(ID2D1Device1* pointer) : base((ID2D1Device*)pointer)
        {
        }

        public new ID2D1Device1* Pointer => (ID2D1Device1*)base.Pointer;

        public D2D1_RENDERING_PRIORITY GetRenderingPriority()
        {
            return Pointer->GetRenderingPriority();
        }

        public void SetRenderingPriority(D2D1_RENDERING_PRIORITY renderingPriority)
        {
            Pointer->SetRenderingPriority(renderingPriority);
        }

        public static implicit operator ID2D1Device1*(D2D1Device1 value)
        {
            return value.Pointer;
        }
    }
}