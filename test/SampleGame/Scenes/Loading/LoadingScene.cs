using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Game;

namespace BouncyBox.VorpalEngine.SampleGame.Scenes.Loading
{
    public class LoadingScene : Scene
    {
        public const int UpdateOrder = int.MaxValue;
        public const int RenderOrder = int.MaxValue;

        public LoadingScene(IInterfaces interfaces, IGameStateManager<GameState> gameStateManager, IEntityManager<GameState> entityManager)
            : base(interfaces, entityManager, SceneKey.Loading)
        {
            AddEntities(new LoadingEntity(interfaces));
        }
    }
}