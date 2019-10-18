using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX
{
    /// <summary>
    ///     Factory for various Direct2D structs.
    /// </summary>
    public static class D2DFactory
    {
        /// <summary>
        ///     A point at (0,0).
        /// </summary>
        public static readonly D2D_POINT_2F ZeroPoint2F = new D2D_POINT_2F { x = 0, y = 0 };

        /// <summary>
        ///     A point at (0,0).
        /// </summary>
        public static readonly D2D_POINT_2U ZeroPoint2U = new D2D_POINT_2U { x = 0, y = 0 };

        /// <summary>
        ///     A size of (0,0).
        /// </summary>
        public static readonly D2D_SIZE_F ZeroSizeF = new D2D_SIZE_F { width = 0, height = 0 };

        /// <summary>
        ///     A size of (0,0).
        /// </summary>
        public static readonly D2D_SIZE_U ZeroSizeU = new D2D_SIZE_U { width = 0, height = 0 };

        /// <summary>
        ///     Creates a <see cref="D2D_POINT_2F" />.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        /// <returns>Returns a <see cref="D2D_POINT_2F" />.</returns>
        public static D2D_POINT_2F CreatePoint2F(float x, float y)
        {
            return
                new D2D_POINT_2F
                {
                    x = x,
                    y = y
                };
        }

        /// <summary>
        ///     Creates a <see cref="D2D_POINT_2U" />.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        /// <returns>Returns a <see cref="D2D_POINT_2U" />.</returns>
        public static D2D_POINT_2U CreatePoint2U(uint x, uint y)
        {
            return
                new D2D_POINT_2U
                {
                    x = x,
                    y = y
                };
        }

        /// <summary>
        ///     Creates a <see cref="D2D_RECT_F" />.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="top">The top value.</param>
        /// <param name="right">The right value.</param>
        /// <param name="bottom">The bottom value.</param>
        /// <returns>Returns a <see cref="D2D_RECT_F" />.</returns>
        public static D2D_RECT_F CreateRectF(float left, float top, float right, float bottom)
        {
            return
                new D2D_RECT_F
                {
                    left = left,
                    top = top,
                    right = right,
                    bottom = bottom
                };
        }

        /// <summary>
        ///     Creates a <see cref="D2D_RECT_F" />.
        /// </summary>
        /// <param name="topLeft">The top-left location.</param>
        /// <param name="bottomRight">The bottom-right location.</param>
        /// <returns>Returns a <see cref="D2D_RECT_F" />.</returns>
        public static D2D_RECT_F CreateRectF(D2D_POINT_2F topLeft, D2D_POINT_2F bottomRight)
        {
            return
                new D2D_RECT_F
                {
                    left = topLeft.x,
                    top = topLeft.y,
                    right = bottomRight.x,
                    bottom = bottomRight.y
                };
        }

        /// <summary>
        ///     Creates a <see cref="D2D_RECT_F" />.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="size">The size.</param>
        /// <returns>Returns a <see cref="D2D_RECT_F" />.</returns>
        public static D2D_RECT_F CreateRectF(D2D_POINT_2F location, D2D_SIZE_F size)
        {
            return
                new D2D_RECT_F
                {
                    left = location.x,
                    top = location.y,
                    right = location.x + size.width,
                    bottom = location.y + size.height
                };
        }

        /// <summary>
        ///     Creates a <see cref="D2D_RECT_F" />.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="top">The top value.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>Returns a <see cref="D2D_RECT_F" />.</returns>
        public static D2D_RECT_F CreateRectFWithSize(float left, float top, float width, float height)
        {
            return
                new D2D_RECT_F
                {
                    left = left,
                    top = top,
                    right = left + width,
                    bottom = top + height
                };
        }

        /// <summary>
        ///     Creates a <see cref="D2D_RECT_U" />.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="top">The top value.</param>
        /// <param name="right">The right value.</param>
        /// <param name="bottom">The bottom value.</param>
        /// <returns>Returns a <see cref="D2D_RECT_U" />.</returns>
        public static D2D_RECT_U CreateRectU(uint left, uint top, uint right, uint bottom)
        {
            return
                new D2D_RECT_U
                {
                    left = left,
                    top = top,
                    right = right,
                    bottom = bottom
                };
        }

        /// <summary>
        ///     Creates a <see cref="D2D_RECT_U" />.
        /// </summary>
        /// <param name="topLeft">The top-left location.</param>
        /// <param name="bottomRight">The bottom-right location.</param>
        /// <returns>Returns a <see cref="D2D_RECT_U" />.</returns>
        public static D2D_RECT_U CreateRectU(D2D_POINT_2U topLeft, D2D_POINT_2U bottomRight)
        {
            return
                new D2D_RECT_U
                {
                    left = topLeft.x,
                    top = topLeft.y,
                    right = bottomRight.x,
                    bottom = bottomRight.y
                };
        }

        /// <summary>
        ///     Creates a <see cref="D2D_RECT_U" />.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="size">The size.</param>
        /// <returns>Returns a <see cref="D2D_RECT_U" />.</returns>
        public static D2D_RECT_U CreateRectU(D2D_POINT_2U location, D2D_SIZE_U size)
        {
            return
                new D2D_RECT_U
                {
                    left = location.x,
                    top = location.y,
                    right = location.x + size.width,
                    bottom = location.y + size.height
                };
        }

        /// <summary>
        ///     Creates a <see cref="D2D_RECT_U" />.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="top">The top value.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>Returns a <see cref="D2D_RECT_U" />.</returns>
        public static D2D_RECT_U CreateRectUWithSize(uint left, uint top, uint width, uint height)
        {
            return
                new D2D_RECT_U
                {
                    left = left,
                    top = top,
                    right = left + width,
                    bottom = top + height
                };
        }

        /// <summary>
        ///     Creates a <see cref="D2D_SIZE_F" />.
        /// </summary>
        /// <param name="width">The width value.</param>
        /// <param name="height">The height value.</param>
        /// <returns>Returns a <see cref="D2D_SIZE_F" />.</returns>
        public static D2D_SIZE_F CreateSizeF(float width, float height)
        {
            return
                new D2D_SIZE_F
                {
                    width = width,
                    height = height
                };
        }

        /// <summary>
        ///     Creates a <see cref="D2D_SIZE_U" />.
        /// </summary>
        /// <param name="width">The width value.</param>
        /// <param name="height">The height value.</param>
        /// <returns>Returns a <see cref="D2D_SIZE_U" />.</returns>
        public static D2D_SIZE_U CreateSizeU(uint width, uint height)
        {
            return
                new D2D_SIZE_U
                {
                    width = width,
                    height = height
                };
        }
    }
}