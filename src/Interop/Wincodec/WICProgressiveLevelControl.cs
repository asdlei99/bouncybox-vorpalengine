using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICProgressiveLevelControl" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICProgressiveLevelControl : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICProgressiveLevelControl" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICProgressiveLevelControl(IWICProgressiveLevelControl* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICProgressiveLevelControl* Pointer => (IWICProgressiveLevelControl*)base.Pointer;

        public HResult GetCurrentLevel(uint* level)
        {
            return Pointer->GetCurrentLevel(level);
        }

        public HResult GetLevelCount(uint* levels)
        {
            return Pointer->GetLevelCount(levels);
        }

        public HResult SetCurrentLevel(uint level)
        {
            return Pointer->SetCurrentLevel(level);
        }

        public static implicit operator IWICProgressiveLevelControl*(WICProgressiveLevelControl value)
        {
            return value.Pointer;
        }
    }
}