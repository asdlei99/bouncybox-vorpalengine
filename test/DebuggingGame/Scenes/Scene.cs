using BouncyBox.VorpalEngine.DebuggingGame.States.Game;
using BouncyBox.VorpalEngine.DebuggingGame.States.Render;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Scenes;

namespace BouncyBox.VorpalEngine.DebuggingGame.Scenes
{
    public abstract class Scene : Scene<GameState, RenderState, SceneKey>
    {
        protected Scene(IInterfaces interfaces, IEntityManager<GameState, RenderState> entityManager, SceneKey key) : base(interfaces, entityManager, key)
        {
        }
    }
}