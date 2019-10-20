using System;
using System.Diagnostics;
using System.Threading;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.DirectX.ComObjects;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Math;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.SampleGame.Scenes.Loading
{
    public class LoadingEntity : Entity
    {
        private readonly SineWave _loadingIndicatorSineWave;
        private readonly TriangleWave _loadingIndicatorTriangleWave;
        private readonly Stopwatch _opacityStopwatch = new Stopwatch();
        private D2D1SolidColorBrush? _brush;
        private DWriteTextFormat? _textFormat;

        public LoadingEntity(IInterfaces interfaces, NestedContext context) : base(interfaces, 0, 0, context)
        {
            _loadingIndicatorSineWave = new SineWave(0, 1, TimeSpan.FromSeconds(2), WaveOffset.Trough, _opacityStopwatch);
            _loadingIndicatorTriangleWave = new TriangleWave(0, 1, TimeSpan.FromSeconds(2), WaveOffset.Trough, _opacityStopwatch);
        }

        public LoadingEntity(IInterfaces interfaces) : this(interfaces, NestedContext.None())
        {
        }

        protected override void OnUpdateGameState(CancellationToken cancellationToken)
        {
            _opacityStopwatch.Start();
        }

        protected override Action<DirectXResources, CancellationToken> OnGetRenderDelegate()
        {
            float triangleOpacity = _loadingIndicatorTriangleWave.Value;
            float sineOpacity = _loadingIndicatorSineWave.Value;

            return
                (resources, cancellationToken) =>
                {
                    D2D_RECT_F loading1Rect = resources.ClientRect.ToD2DRectF();
                    D2D_RECT_F loading2Rect = loading1Rect;

                    loading2Rect.top += 100;
                    loading2Rect.bottom += 100;

                    _brush!.Opacity = triangleOpacity;
                    resources.D2D1DeviceContext.DrawText("Loading", _textFormat!, loading1Rect, _brush);

                    _brush.Opacity = sineOpacity;
                    resources.D2D1DeviceContext.DrawText("Loading", _textFormat!, loading2Rect, _brush);
                };
        }

        protected override unsafe void OnInitializeRenderResources(DirectXResources resources)
        {
            DXGI_RGBA brushColor = DXGIFactory.CreateRgba(1, 1, 1, 1);

            _brush = resources.D2D1DeviceContext.CreateSolidColorBrush(brushColor);
            _textFormat = resources.DWriteFactory1.CreateTextFormat("Calibri", 96);
        }

        protected override void OnReleaseRenderResources()
        {
            _brush?.Dispose();
            _textFormat?.Dispose();
        }
    }
}