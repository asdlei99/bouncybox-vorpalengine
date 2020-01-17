using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX
{
    /// <summary>Extensions for the <see cref="D2D_SIZE_U" /> type.</summary>
    public static class D2DSizeUExtensions
    {
        /// <summary>Converts the <see cref="D2D_SIZE_U" /> to a <see cref="D2D_SIZE_U" />.</summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns the value converted to a <see cref="D2D_POINT_2F" />.</returns>
        public static D2D_SIZE_F ToD2DSizeF(this D2D_SIZE_U value)
        {
            return new D2D_SIZE_F(value.width, value.height);
        }
    }
}