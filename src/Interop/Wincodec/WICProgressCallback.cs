using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICProgressCallback" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe class WICProgressCallback : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICProgressCallback" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICProgressCallback(IWICProgressCallback* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICProgressCallback* Pointer => (IWICProgressCallback*)base.Pointer;

        public HResult Notify(uint frameNum, WICProgressOperation operation, double progress)
        {
            return Pointer->Notify(frameNum, operation, progress);
        }

        public static implicit operator IWICProgressCallback*(WICProgressCallback value)
        {
            return value.Pointer;
        }
    }
}