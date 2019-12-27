using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICMetadataQueryWriter" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICMetadataQueryWriter : WICMetadataQueryReader
    {
        /// <summary>Initializes a new instance of the <see cref="WICMetadataQueryWriter" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICMetadataQueryWriter(IWICMetadataQueryWriter* pointer) : base((IWICMetadataQueryReader*)pointer)
        {
        }

        public new IWICMetadataQueryWriter* Pointer => (IWICMetadataQueryWriter*)base.Pointer;

        public HResult RemoveMetadataByName(ushort* name)
        {
            return Pointer->RemoveMetadataByName(name);
        }

        public HResult SetMetadataByName(ushort* name, PROPVARIANT* value)
        {
            return Pointer->SetMetadataByName(name, value);
        }

        public static implicit operator IWICMetadataQueryWriter*(WICMetadataQueryWriter value)
        {
            return value.Pointer;
        }
    }
}