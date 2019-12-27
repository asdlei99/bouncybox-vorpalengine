using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using BouncyBox.VorpalEngine.Interop.ObjIdl;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICStream" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICStream : Stream
    {
        /// <summary>Initializes a new instance of the <see cref="WICStream" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICStream(IWICStream* pointer) : base((IStream*)pointer)
        {
        }

        public new IWICStream* Pointer => (IWICStream*)base.Pointer;

        public HResult InitializeFromFilename(ushort* fileName, uint desiredAccess)
        {
            return Pointer->InitializeFromFilename(fileName, desiredAccess);
        }

        public HResult InitializeFromIStream(IStream* stream = null)
        {
            return Pointer->InitializeFromIStream(stream);
        }

        public HResult InitializeFromIStreamRegion([Optional] IStream* stream, ULARGE_INTEGER offset, ULARGE_INTEGER maxSize)
        {
            return Pointer->InitializeFromIStreamRegion(stream, offset, maxSize);
        }

        public HResult InitializeFromMemory(byte* buffer, uint bufferSize)
        {
            return Pointer->InitializeFromMemory(buffer, bufferSize);
        }

        public static implicit operator IWICStream*(WICStream value)
        {
            return value.Pointer;
        }
    }
}