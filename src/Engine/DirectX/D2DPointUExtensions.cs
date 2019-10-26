using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX
{
    /// <summary>Extensions for the <see cref="D2D_POINT_2U" /> type.</summary>
    public static class D2DPointUExtensions
    {
        /// <summary>Converts the <see cref="D2D_POINT_2U" /> to a <see cref="D2D_POINT_2U" />.</summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns the value converted to a <see cref="D2D_POINT_2F" />.</returns>
        public static D2D_POINT_2F ToD2DPoint2F(this D2D_POINT_2U value)
        {
            return
                new D2D_POINT_2F
                {
                    x = value.x,
                    y = value.y
                };
        }
    }
}