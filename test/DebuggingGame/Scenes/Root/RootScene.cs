using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Game;

namespace BouncyBox.VorpalEngine.DebuggingGame.Scenes.Root
{
    public class RootScene : Scene
    {
        private readonly IGameStateManager<GameState> _gameStateManager;

        public RootScene(IInterfaces interfaces, IGameStateManager<GameState> gameStateManager, IEntityManager<GameState> entityManager)
            : base(interfaces, entityManager, SceneKey.Root)
        {
            _gameStateManager = gameStateManager;

            AddEntities(new RootEntity(interfaces, gameStateManager));
        }

        protected override void OnLoad()
        {
            _gameStateManager.GameState.SceneStates.Root ??= new RootSceneGameState();
        }

        protected override void OnUnload()
        {
            _gameStateManager.GameState.SceneStates.Root = null;
        }
    }
}