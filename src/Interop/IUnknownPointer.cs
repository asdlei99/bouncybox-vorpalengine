using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe struct IUnknownPointer
    {
        public IUnknown* Pointer;

        public static implicit operator IUnknownPointer(IUnknown* value)
        {
            return
                new IUnknownPointer
                {
                    Pointer = value
                };
        }
    }
}