using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX
{
    /// <summary>Extensions for the <see cref="D2D_RECT_U" /> type.</summary>
    public static class D2DRectUExtensions
    {
        /// <summary>Converts the <see cref="D2D_RECT_U" /> to a <see cref="D2D_RECT_F" />.</summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns the value converted to a <see cref="D2D_RECT_F" />.</returns>
        public static D2D_RECT_F ToD2DRectF(this in D2D_RECT_U value)
        {
            return new D2D_RECT_F(value.left, value.top, value.right, value.bottom);
        }
    }
}