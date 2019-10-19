using System.Threading;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.SampleGame.Entities.Renderers;
using BouncyBox.VorpalEngine.SampleGame.States.Render;

namespace BouncyBox.VorpalEngine.SampleGame.Scenes.Title
{
    public class TitleRenderer : Renderer
    {
        public TitleRenderer(IInterfaces interfaces) : base(interfaces)
        {
        }

        protected override void OnRender(DirectXResources resources, RenderState renderState, CancellationToken cancellationToken)
        {
        }
    }
}