using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_1
{
    /// <summary>Proxies the <see cref="ID2D1Multithread" /> COM interface.</summary>
    public unsafe class D2D1Multithread : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Multithread" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Multithread(ID2D1Multithread* pointer) : base((IUnknown*)pointer)
        {
        }

        public new ID2D1Multithread* Pointer => (ID2D1Multithread*)base.Pointer;

        public void Enter()
        {
            Pointer->Enter();
        }

        public bool GetMultithreadProtected()
        {
            return Pointer->GetMultithreadProtected() == TerraFX.Interop.Windows.TRUE;
        }

        public void Leave()
        {
            Pointer->Leave();
        }

        public static implicit operator ID2D1Multithread*(D2D1Multithread value)
        {
            return value.Pointer;
        }
    }
}