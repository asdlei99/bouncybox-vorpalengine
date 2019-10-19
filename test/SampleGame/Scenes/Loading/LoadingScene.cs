using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.SampleGame.States.Game;
using BouncyBox.VorpalEngine.SampleGame.States.Render;

namespace BouncyBox.VorpalEngine.SampleGame.Scenes.Loading
{
    public class LoadingScene : Scene
    {
        public LoadingScene(IInterfaces interfaces, IEntityManager<GameState, RenderState> entityManager) : base(interfaces, entityManager, SceneKey.Root)
        {
            AddUpdaters(new LoadingUpdater(interfaces));
            AddRenderers(new LoadingRenderer(interfaces));
        }
    }
}