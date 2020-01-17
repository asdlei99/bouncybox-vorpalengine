using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX
{
    /// <summary>Factory for various Direct2D structs.</summary>
    public static class D2DFactory
    {
        /// <summary>A point at (0,0).</summary>
        public static readonly D2D_POINT_2F ZeroPoint2F = new D2D_POINT_2F();

        /// <summary>A point at (0,0).</summary>
        public static readonly D2D_POINT_2U ZeroPoint2U = new D2D_POINT_2U();

        /// <summary>A size of (0,0).</summary>
        public static readonly D2D_SIZE_F ZeroSizeF = new D2D_SIZE_F();

        /// <summary>A size of (0,0).</summary>
        public static readonly D2D_SIZE_U ZeroSizeU = new D2D_SIZE_U();

        /// <summary>Creates a <see cref="D2D_RECT_F" />.</summary>
        /// <param name="topLeft">The top-left location.</param>
        /// <param name="bottomRight">The bottom-right location.</param>
        /// <returns>Returns a <see cref="D2D_RECT_F" />.</returns>
        public static D2D_RECT_F CreateRectF(D2D_POINT_2F topLeft, D2D_POINT_2F bottomRight)
        {
            return new D2D_RECT_F(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
        }

        /// <summary>Creates a <see cref="D2D_RECT_F" />.</summary>
        /// <param name="location">The location.</param>
        /// <param name="size">The size.</param>
        /// <returns>Returns a <see cref="D2D_RECT_F" />.</returns>
        public static D2D_RECT_F CreateRectF(D2D_POINT_2F location, D2D_SIZE_F size)
        {
            return new D2D_RECT_F(location.x, location.y, location.x + size.width, location.y + size.height);
        }

        /// <summary>Creates a <see cref="D2D_RECT_F" />.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="top">The top value.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>Returns a <see cref="D2D_RECT_F" />.</returns>
        public static D2D_RECT_F CreateRectF(float left, float top, float width, float height)
        {
            return new D2D_RECT_F(left, top, left + width, top + height);
        }

        /// <summary>Creates a <see cref="D2D_RECT_U" />.</summary>
        /// <param name="topLeft">The top-left location.</param>
        /// <param name="bottomRight">The bottom-right location.</param>
        /// <returns>Returns a <see cref="D2D_RECT_U" />.</returns>
        public static D2D_RECT_U CreateRectU(D2D_POINT_2U topLeft, D2D_POINT_2U bottomRight)
        {
            return new D2D_RECT_U(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
        }

        /// <summary>Creates a <see cref="D2D_RECT_U" />.</summary>
        /// <param name="location">The location.</param>
        /// <param name="size">The size.</param>
        /// <returns>Returns a <see cref="D2D_RECT_U" />.</returns>
        public static D2D_RECT_U CreateRectU(D2D_POINT_2U location, D2D_SIZE_U size)
        {
            return new D2D_RECT_U(location.x, location.y, location.x + size.width, location.y + size.height);
        }

        /// <summary>Creates a <see cref="D2D_RECT_U" />.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="top">The top value.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>Returns a <see cref="D2D_RECT_U" />.</returns>
        public static D2D_RECT_U CreateRectU(uint left, uint top, uint width, uint height)
        {
            return new D2D_RECT_U(left, top, left + width, top + height);
        }
    }
}