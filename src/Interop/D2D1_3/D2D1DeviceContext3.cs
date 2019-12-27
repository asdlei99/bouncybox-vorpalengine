using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1DeviceContext3" /> COM interface.</summary>
    public unsafe partial class D2D1DeviceContext3 : D2D1DeviceContext2
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1DeviceContext3" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1DeviceContext3(ID2D1DeviceContext3* pointer) : base((ID2D1DeviceContext2*)pointer)
        {
        }

        public new ID2D1DeviceContext3* Pointer => (ID2D1DeviceContext3*)base.Pointer;

        public HResult CreateSpriteBatch(ID2D1SpriteBatch** spriteBatch)
        {
            return Pointer->CreateSpriteBatch(spriteBatch);
        }

        public void DrawSpriteBatch(
            ID2D1SpriteBatch* spriteBatch,
            uint startIndex,
            uint spriteCount,
            ID2D1Bitmap* bitmap,
            D2D1_BITMAP_INTERPOLATION_MODE interpolationMode = D2D1_BITMAP_INTERPOLATION_MODE.D2D1_BITMAP_INTERPOLATION_MODE_LINEAR,
            D2D1_SPRITE_OPTIONS spriteOptions = D2D1_SPRITE_OPTIONS.D2D1_SPRITE_OPTIONS_NONE)
        {
            Pointer->DrawSpriteBatch(spriteBatch, startIndex, spriteCount, bitmap, interpolationMode, spriteOptions);
        }

        public void DrawSpriteBatch(
            ID2D1SpriteBatch* spriteBatch,
            ID2D1Bitmap* bitmap,
            D2D1_BITMAP_INTERPOLATION_MODE interpolationMode = D2D1_BITMAP_INTERPOLATION_MODE.D2D1_BITMAP_INTERPOLATION_MODE_LINEAR,
            D2D1_SPRITE_OPTIONS spriteOptions = D2D1_SPRITE_OPTIONS.D2D1_SPRITE_OPTIONS_NONE)
        {
            Pointer->DrawSpriteBatch(spriteBatch, bitmap, interpolationMode, spriteOptions);
        }

        public static implicit operator ID2D1DeviceContext3*(D2D1DeviceContext3 value)
        {
            return value.Pointer;
        }
    }
}