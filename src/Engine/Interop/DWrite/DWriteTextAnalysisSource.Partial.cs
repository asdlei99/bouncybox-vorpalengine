using System;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    public unsafe partial class DWriteTextAnalysisSource
    {
        public HResult GetLocaleName(uint textPosition, out ReadOnlySpan<char> localeName)
        {
            uint textLength;
            ushort* pLocaleName;
            int hr = Pointer->GetLocaleName(textPosition, &textLength, &pLocaleName);

            localeName = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new ReadOnlySpan<char>(pLocaleName, checked((int)textLength)) : default;

            return hr;
        }

        public HResult GetNumberSubstitution(uint textPosition, out uint textLength, out DWriteNumberSubstitution? numberSubstitution)
        {
            IDWriteNumberSubstitution* pNumberSubstitution;
            int hr;

            fixed (uint* pTextLength = &textLength)
            {
                hr = Pointer->GetNumberSubstitution(textPosition, pTextLength, &pNumberSubstitution);
            }

            numberSubstitution = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteNumberSubstitution(pNumberSubstitution) : null;

            return hr;
        }

        public HResult GetTextAtPosition(uint textPosition, out ReadOnlySpan<ushort> textString)
        {
            ushort* pTextString;
            uint textLength;
            int hr = Pointer->GetTextAtPosition(textPosition, &pTextString, &textLength);

            textString = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new ReadOnlySpan<ushort>(pTextString, checked((int)textLength)) : null;

            return hr;
        }

        public HResult GetTextBeforePosition(uint textPosition, out ReadOnlySpan<ushort> textString)
        {
            ushort* pTextString;
            uint textLength;
            int hr = Pointer->GetTextBeforePosition(textPosition, &pTextString, &textLength);

            textString = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new ReadOnlySpan<ushort>(pTextString, checked((int)textLength)) : null;

            return hr;
        }
    }
}