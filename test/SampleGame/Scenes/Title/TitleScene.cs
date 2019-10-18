using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.SampleGame.States.Game;
using BouncyBox.VorpalEngine.SampleGame.States.Render;

namespace BouncyBox.VorpalEngine.SampleGame.Scenes.Title
{
    public class TitleScene : Scene
    {
        public TitleScene(IInterfaces interfaces, IEntityManager<GameState, RenderState> entityManager) : base(interfaces, entityManager, SceneKey.Title)
        {
            AddRenderers(new TitleRenderer(interfaces));
        }
    }
}