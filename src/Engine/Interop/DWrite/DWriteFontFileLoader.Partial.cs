using System;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    public unsafe partial class DWriteFontFileLoader
    {
        public HResult CreateStreamFromKey(ReadOnlySpan<byte> fontFileReferenceKey, out DWriteFontFileStream? fontFileStream)
        {
            IDWriteFontFileStream* pFontFileStream;
            int hr;

            fixed (byte* pFontFileReferenceKey = fontFileReferenceKey)
            {
                hr = Pointer->CreateStreamFromKey(pFontFileReferenceKey, (uint)fontFileReferenceKey.Length, &pFontFileStream);
            }

            fontFileStream = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFontFileStream(pFontFileStream) : null;

            return hr;
        }
    }
}