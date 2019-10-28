using BouncyBox.VorpalEngine.Common;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Entities;

namespace BouncyBox.VorpalEngine.SampleGame.Scenes.Title
{
    public class TitleEntity : UpdatingRenderingEntity<TitleEntityRenderState>
    {
        public TitleEntity(IInterfaces interfaces, NestedContext context) : base(interfaces, 0, 0, context)
        {
        }

        public TitleEntity(IInterfaces interfaces) : this(interfaces, NestedContext.None())
        {
        }
    }
}