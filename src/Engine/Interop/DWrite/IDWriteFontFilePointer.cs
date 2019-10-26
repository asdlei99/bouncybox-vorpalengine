using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe struct IDWriteFontFilePointer
    {
        public IDWriteFontFile* Pointer;

        public static implicit operator IDWriteFontFilePointer(IDWriteFontFile* value)
        {
            return
                new IDWriteFontFilePointer
                {
                    Pointer = value
                };
        }
    }
}