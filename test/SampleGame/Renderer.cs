using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Entities.Renderers;
using BouncyBox.VorpalEngine.SampleGame.States.Render;

namespace BouncyBox.VorpalEngine.SampleGame
{
    public abstract class Renderer : Renderer<RenderState>
    {
        protected Renderer(IInterfaces interfaces, NestedContext context, uint order = 0) : base(interfaces, context, order)
        {
        }

        protected Renderer(IInterfaces interfaces, uint order = 0) : base(interfaces, order)
        {
        }
    }
}