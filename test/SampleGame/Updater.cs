using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Entities.Updaters;
using BouncyBox.VorpalEngine.SampleGame.States.Game;
using BouncyBox.VorpalEngine.SampleGame.States.Render;

namespace BouncyBox.VorpalEngine.SampleGame
{
    public abstract class Updater : Updater<GameState, RenderState>
    {
        protected Updater(IInterfaces interfaces, NestedContext context, uint order = 0) : base(interfaces, context, order)
        {
        }

        protected Updater(IInterfaces interfaces, uint order = 0) : base(interfaces, order)
        {
        }
    }
}