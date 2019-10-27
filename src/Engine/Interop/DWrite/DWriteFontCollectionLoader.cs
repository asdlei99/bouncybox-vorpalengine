using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteFontCollectionLoader" /> COM interface.</summary>
    public unsafe partial class DWriteFontCollectionLoader : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteFontCollectionLoader" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteFontCollectionLoader(IDWriteFontCollectionLoader* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteFontCollectionLoader* Pointer => (IDWriteFontCollectionLoader*)base.Pointer;

        public HResult CreateEnumeratorFromKey(
            IDWriteFactory* factory,
            void* collectionKey,
            uint collectionKeySize,
            IDWriteFontFileEnumerator** fontFileEnumerator)
        {
            return Pointer->CreateEnumeratorFromKey(factory, collectionKey, collectionKeySize, fontFileEnumerator);
        }

        public static implicit operator IDWriteFontCollectionLoader*(DWriteFontCollectionLoader value)
        {
            return value.Pointer;
        }
    }
}