using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICColorContext" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICColorContext : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICColorContext" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICColorContext(IWICColorContext* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICColorContext* Pointer => (IWICColorContext*)base.Pointer;

        public HResult GetExifColorSpace(uint* value)
        {
            return Pointer->GetExifColorSpace(value);
        }

        public HResult GetProfileBytes(uint bufferSize, byte* buffer, uint* actual)
        {
            return Pointer->GetProfileBytes(bufferSize, buffer, actual);
        }

        public HResult GetType(WICColorContextType* type)
        {
            return Pointer->GetType(type);
        }

        public HResult InitializeFromExifColorSpace(uint value)
        {
            return Pointer->InitializeFromExifColorSpace(value);
        }

        public HResult InitializeFromFilename(ushort* filename)
        {
            return Pointer->InitializeFromFilename(filename);
        }

        public HResult InitializeFromMemory(byte* buffer, uint bufferSize)
        {
            return Pointer->InitializeFromMemory(buffer, bufferSize);
        }

        public static implicit operator IWICColorContext*(WICColorContext value)
        {
            return value.Pointer;
        }
    }
}