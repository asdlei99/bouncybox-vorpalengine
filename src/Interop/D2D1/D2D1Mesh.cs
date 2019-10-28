using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1Mesh" /> COM interface.</summary>
    public unsafe partial class D2D1Mesh : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Mesh" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Mesh(ID2D1Mesh* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1Mesh* Pointer => (ID2D1Mesh*)base.Pointer;

        public HResult Open(ID2D1TessellationSink** tessellationSink)
        {
            return Pointer->Open(tessellationSink);
        }

        public static implicit operator ID2D1Mesh*(D2D1Mesh value)
        {
            return value.Pointer;
        }
    }
}