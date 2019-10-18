using BouncyBox.VorpalEngine.DebuggingGame.States.Render;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Entities.Updaters;

namespace BouncyBox.VorpalEngine.DebuggingGame
{
    public abstract class Updater : Updater<RenderState>
    {
        protected Updater(IInterfaces interfaces, NestedContext context, uint order = 0) : base(interfaces, context, order)
        {
        }

        protected Updater(IInterfaces interfaces, uint order = 0) : base(interfaces, order)
        {
        }
    }
}