using BouncyBox.VorpalEngine.Engine.DirectX.ComObjects;
using BouncyBox.VorpalEngine.Engine.Interop;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX
{
    /// <summary>
    ///     Provides core DirectX resources and render window client area dimensions.
    /// </summary>
    public struct DirectXResources
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DirectXResources" /> type.
        /// </summary>
        /// <param name="dxgiAdapter">An <see cref="IDXGIAdapter" />.</param>
        /// <param name="d2d1DeviceContext">An <see cref="ID2D1DeviceContext" />.</param>
        /// <param name="dWriteFactory1">An <see cref="IDWriteFactory1" />.</param>
        /// <param name="clientSize">The size of the render window's client area.</param>
        public DirectXResources(DXGIAdapter dxgiAdapter, D2D1DeviceContext d2d1DeviceContext, DWriteFactory1 dWriteFactory1, D2D_SIZE_U clientSize)
        {
            DXGIAdapter = dxgiAdapter;
            D2D1DeviceContext = d2d1DeviceContext;
            DWriteFactory1 = dWriteFactory1;
            ClientSize = clientSize;
            ClientRect = D2DFactory.CreateRectU(D2DFactory.ZeroPoint2U, clientSize);
        }

        /// <summary>
        ///     <para>Initializes a new instance of the <see cref="DirectXResources" /> type.</para>
        ///     <para>
        ///         This constructor is intended to be used when the core DirectX resources are unchanged but the render window's client
        ///         area changes.
        ///     </para>
        /// </summary>
        /// <param name="resources">A <see cref="DirectXResources" /> to copy.</param>
        /// <param name="clientSize">The size of the render window's client area.</param>
        public DirectXResources(DirectXResources resources, D2D_SIZE_U clientSize)
            : this(resources.DXGIAdapter, resources.D2D1DeviceContext, resources.DWriteFactory1, clientSize)
        {
        }

        /// <summary>
        ///     Gets the <see cref="DXGIAdapter" />.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public DXGIAdapter DXGIAdapter { get; }

        /// <summary>
        ///     Gets the <see cref="D2D1DeviceContext" />.
        /// </summary>
        public D2D1DeviceContext D2D1DeviceContext { get; }

        /// <summary>
        ///     Gets the <see cref="DWriteFactory1" />.
        /// </summary>
        public DWriteFactory1 DWriteFactory1 { get; }

        /// <summary>
        ///     Gets the size of the render window's client area.
        /// </summary>
        public D2D_SIZE_U ClientSize { get; }

        /// <summary>
        ///     Gets the render window's client rectangle.
        /// </summary>
        public D2D_RECT_U ClientRect { get; }
    }
}