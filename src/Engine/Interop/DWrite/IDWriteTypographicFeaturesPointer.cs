using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe struct IDWriteTypographicFeaturesPointer
    {
        public DWRITE_TYPOGRAPHIC_FEATURES* Pointer;

        public static implicit operator IDWriteTypographicFeaturesPointer(DWRITE_TYPOGRAPHIC_FEATURES* value)
        {
            return
                new IDWriteTypographicFeaturesPointer
                {
                    Pointer = value
                };
        }
    }
}