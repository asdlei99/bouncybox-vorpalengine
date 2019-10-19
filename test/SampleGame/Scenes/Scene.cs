using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Scenes;
using BouncyBox.VorpalEngine.SampleGame.States.Game;
using BouncyBox.VorpalEngine.SampleGame.States.Render;

namespace BouncyBox.VorpalEngine.SampleGame.Scenes
{
    public abstract class Scene : Scene<GameState, RenderState, SceneKey>
    {
        protected Scene(IInterfaces interfaces, IEntityManager<GameState, RenderState> entityManager, SceneKey key) : base(interfaces, entityManager, key)
        {
        }
    }
}