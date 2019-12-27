using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICProgressiveLevelControl
    {
        public HResult GetCurrentLevel(out uint level)
        {
            fixed (uint* pLevel = &level)
            {
                return Pointer->GetCurrentLevel(pLevel);
            }
        }

        public HResult GetLevelCount(out uint levels)
        {
            fixed (uint* pLevels = &levels)
            {
                return Pointer->GetLevelCount(pLevels);
            }
        }
    }
}