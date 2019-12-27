using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1CommandSink3" /> COM interface.</summary>
    public unsafe class D2D1CommandSink3 : D2D1CommandSink2
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1CommandSink3" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1CommandSink3(ID2D1CommandSink3* pointer) : base((ID2D1CommandSink2*)pointer)
        {
        }

        public new ID2D1CommandSink3* Pointer => (ID2D1CommandSink3*)base.Pointer;

        public HResult DrawSpriteBatch(
            ID2D1SpriteBatch* spriteBatch,
            uint startIndex,
            uint spriteCount,
            ID2D1Bitmap* bitmap,
            D2D1_BITMAP_INTERPOLATION_MODE interpolationMode,
            D2D1_SPRITE_OPTIONS spriteOptions)
        {
            return Pointer->DrawSpriteBatch(spriteBatch, startIndex, spriteCount, bitmap, interpolationMode, spriteOptions);
        }

        public static implicit operator ID2D1CommandSink3*(D2D1CommandSink3 value)
        {
            return value.Pointer;
        }
    }
}