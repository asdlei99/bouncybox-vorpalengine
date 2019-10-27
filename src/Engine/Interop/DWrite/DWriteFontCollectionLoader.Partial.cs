using System;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    public unsafe partial class DWriteFontCollectionLoader
    {
        public HResult CreateEnumeratorFromKey(IDWriteFactory* factory, ReadOnlySpan<byte> collectionKey, out DWriteFontFileEnumerator? fontFileEnumerator)
        {
            IDWriteFontFileEnumerator* pFontFileEnumerator;
            int hr;

            fixed (byte* pCollectionKey = collectionKey)
            {
                hr = Pointer->CreateEnumeratorFromKey(factory, pCollectionKey, (uint)collectionKey.Length, &pFontFileEnumerator);
            }

            fontFileEnumerator = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFontFileEnumerator(pFontFileEnumerator) : null;

            return hr;
        }
    }
}