﻿using BouncyBox.VorpalEngine.Interop.D2D1_1;
using BouncyBox.VorpalEngine.Interop.DWrite;
using BouncyBox.VorpalEngine.Interop.DXGI;
using BouncyBox.VorpalEngine.Interop.DXGI1_2;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX
{
    /// <summary>Provides DirectX resources and render window client area dimensions.</summary>
    public readonly struct DirectXResources
    {
        /// <summary>Initializes a new instance of the <see cref="DirectXResources" /> type.</summary>
        /// <param name="dxgiAdapter">A <see cref="DXGIAdapter" />.</param>
        /// <param name="dxgiSwapChain1">A <see cref="DXGISwapChain1" />.</param>
        /// <param name="d2d1Device">A <see cref="D2D1Device" />.</param>
        /// <param name="d2d1DeviceContext">A <see cref="D2D1DeviceContext" />.</param>
        /// <param name="dWriteFactory">A <see cref="DWriteFactory" />.</param>
        /// <param name="clientSize">The size of the render window's client area.</param>
        public DirectXResources(
            DXGIAdapter dxgiAdapter,
            DXGISwapChain1 dxgiSwapChain1,
            D2D1Device d2d1Device,
            D2D1DeviceContext d2d1DeviceContext,
            DWriteFactory dWriteFactory,
            D2D_SIZE_U clientSize)
        {
            DXGIAdapter = dxgiAdapter;
            DXGISwapChain1 = dxgiSwapChain1;
            D2D1Device = d2d1Device;
            D2D1DeviceContext = d2d1DeviceContext;
            DWriteFactory = dWriteFactory;
            ClientSize = clientSize;
            ClientSizeF = clientSize.ToD2DSizeF();
            ClientRect = D2DFactory.CreateRectU(D2DFactory.ZeroPoint2U, clientSize);
            ClientRectF = ClientRect.ToD2DRectF();
        }

        /// <summary>Gets the <see cref="DXGIAdapter" />.</summary>
        // ReSharper disable once InconsistentNaming
        public DXGIAdapter DXGIAdapter { get; }

        /// <summary>Gets the DXGISwapChain1.</summary>
        // ReSharper disable once InconsistentNaming
        public DXGISwapChain1 DXGISwapChain1 { get; }

        /// <summary>Gets the D2D1Device.</summary>
        public D2D1Device D2D1Device { get; }

        /// <summary>Gets the <see cref="D2D1DeviceContext" />.</summary>
        public D2D1DeviceContext D2D1DeviceContext { get; }

        /// <summary>Gets the <see cref="DWriteFactory" />.</summary>
        public DWriteFactory DWriteFactory { get; }

        /// <summary>Gets the size of the render window's client area.</summary>
        public D2D_SIZE_U ClientSize { get; }

        /// <summary>Gets the size of the render window's client area.</summary>
        public D2D_SIZE_F ClientSizeF { get; }

        /// <summary>Gets the render window's client rectangle.</summary>
        public D2D_RECT_U ClientRect { get; }

        /// <summary>Gets the render window's client rectangle.</summary>
        public D2D_RECT_F ClientRectF { get; }
    }
}