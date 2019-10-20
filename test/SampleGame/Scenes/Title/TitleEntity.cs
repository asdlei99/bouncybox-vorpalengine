using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Entities;

namespace BouncyBox.VorpalEngine.SampleGame.Scenes.Title
{
    public class TitleEntity : Entity
    {
        public TitleEntity(IInterfaces interfaces, NestedContext context) : base(interfaces, 1, 1, context)
        {
        }

        public TitleEntity(IInterfaces interfaces) : this(interfaces, NestedContext.None())
        {
        }
    }
}