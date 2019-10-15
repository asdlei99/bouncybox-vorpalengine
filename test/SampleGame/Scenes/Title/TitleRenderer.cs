using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.SampleGame.States.Render;

namespace BouncyBox.VorpalEngine.SampleGame.Scenes.Title
{
    public class TitleRenderer : Renderer
    {
        private const string Title = "Sample Game";

        public TitleRenderer(IInterfaces interfaces) : base(interfaces)
        {
        }

        /*
        protected override void OnInitializeResources(DirectXResources resources)
        {
            _dWriteTextFormat = resources.DWriteFactory1.CreateTextFormat("Calibri", 144);
            _dWriteTextLayout = resources.DWriteFactory1.CreateTextLayout(Title, Title.Length, _dWriteTextFormat, resources.ClientSize.Width, resources.ClientSize.Height);
            _d2d1SolidColorBrush = resources.D2D1DeviceContext.CreateSolidColorBrush(new Color4(1, 1, 1, 1));
        }

        protected override void OnReleaseResources()
        {
            _dWriteTextFormat?.Dispose();
            _dWriteTextLayout?.Dispose();
            _d2d1SolidColorBrush?.Dispose();
        }

        protected override void OnRender(DirectXResources resources, RenderState renderState, IEngineStats engineStats)
        {
            Debug.Assert(_dWriteTextFormat != null);
            Debug.Assert(_dWriteTextLayout != null);
            Debug.Assert(_d2d1SolidColorBrush != null);

            TextMetrics textMetrics = _dWriteTextLayout.Metrics;
            var layoutRect = new RectF(
                resources.ClientSize.Width / 2f - textMetrics.Width / 2,
                resources.ClientSize.Height / 2f - textMetrics.Height / 2,
                textMetrics.Width,
                textMetrics.Height);

            resources.D2D1DeviceContext.DrawText("Sample Game", _dWriteTextFormat, layoutRect, _d2d1SolidColorBrush);
        }
    */
        protected override void OnRender(DirectXResources resources, RenderState renderState, IEngineStats engineStats)
        {
        }
    }
}