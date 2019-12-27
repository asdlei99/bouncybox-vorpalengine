using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICPixelFormatInfo2
    {
        public HResult GetNumericRepresentation(out WICPixelFormatNumericRepresentation numericRepresentation)
        {
            fixed (WICPixelFormatNumericRepresentation* pNumericRepresentation = &numericRepresentation)
            {
                return Pointer->GetNumericRepresentation(pNumericRepresentation);
            }
        }
    }
}