using BouncyBox.VorpalEngine.Engine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1_1
{
    /// <summary>Proxies the <see cref="ID2D1CommandList" /> COM interface.</summary>
    public unsafe class D2D1CommandList : D2D1Image
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1CommandList" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1CommandList(ID2D1CommandList* pointer) : base((ID2D1Image*)pointer)
        {
        }

        public new ID2D1CommandList* Pointer => (ID2D1CommandList*)base.Pointer;

        public HResult Close()
        {
            return Pointer->Close();
        }

        public HResult Stream(ID2D1CommandSink* sink)
        {
            return Pointer->Stream(sink);
        }

        public static implicit operator ID2D1CommandList*(D2D1CommandList value)
        {
            return value.Pointer;
        }
    }
}