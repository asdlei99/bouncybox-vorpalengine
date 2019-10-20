using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Scenes;

namespace BouncyBox.VorpalEngine.SampleGame.Scenes
{
    public abstract class Scene : Scene<GameState, SceneKey>
    {
        protected Scene(IInterfaces interfaces, IEntityManager<GameState> entityManager, SceneKey key) : base(interfaces, entityManager, key)
        {
        }
    }
}