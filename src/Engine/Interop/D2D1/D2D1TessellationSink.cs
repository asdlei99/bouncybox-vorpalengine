using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1TessellationSink" /> COM interface.</summary>
    public unsafe partial class D2D1TessellationSink : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1TessellationSink" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1TessellationSink(ID2D1TessellationSink* pointer) : base((IUnknown*)pointer)
        {
        }

        public new ID2D1TessellationSink* Pointer => (ID2D1TessellationSink*)base.Pointer;

        public void AddTriangles(D2D1_TRIANGLE* triangles, uint trianglesCount)
        {
            Pointer->AddTriangles(triangles, trianglesCount);
        }

        public HResult Close()
        {
            return Pointer->Close();
        }

        public static implicit operator ID2D1TessellationSink*(D2D1TessellationSink value)
        {
            return value.Pointer;
        }
    }
}