using System.Diagnostics;
using System.Threading;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.DirectX.ComObjects;
using BouncyBox.VorpalEngine.SampleGame.States.Render;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.SampleGame.Scenes.Root
{
    public class LoadingRenderer : Renderer
    {
        private D2D1SolidColorBrush? _brush;
        private DWriteTextFormat? _textFormat;

        public LoadingRenderer(IInterfaces interfaces, NestedContext context) : base(interfaces, context.CopyAndPush(nameof(LoadingRenderer)))
        {
        }

        public LoadingRenderer(IInterfaces interfaces) : base(interfaces)
        {
        }

        protected override unsafe void OnInitializeResources(DirectXResources resources)
        {
            DXGI_RGBA brushColor = DXGIFactory.CreateRgba(1, 1, 1, 1);

            _brush = resources.D2D1DeviceContext.CreateSolidColorBrush(brushColor);
            _textFormat = resources.DWriteFactory1.CreateTextFormat("Calibri", 96);
        }

        protected override void OnReleaseResources()
        {
            _brush?.Dispose();
            _textFormat?.Dispose();
        }

        protected override void OnRender(DirectXResources resources, RenderState renderState, CancellationToken cancellationToken)
        {
            Debug.Assert(renderState.SceneStates.Loading != null);

            LoadingSceneRenderState sceneRenderState = renderState.SceneStates.Loading;

            D2D_RECT_F loading1Rect = resources.ClientRect.ToD2DRectF();
            D2D_RECT_F loading2Rect = loading1Rect;

            loading2Rect.top += 100;
            loading2Rect.bottom += 100;

            _brush!.Opacity = sceneRenderState.TriangleOpacity;
            resources.D2D1DeviceContext.DrawText("Loading", _textFormat!, loading1Rect, _brush);

            _brush.Opacity = sceneRenderState.SineOpacity;
            resources.D2D1DeviceContext.DrawText("Loading", _textFormat!, loading2Rect, _brush);
        }
    }
}