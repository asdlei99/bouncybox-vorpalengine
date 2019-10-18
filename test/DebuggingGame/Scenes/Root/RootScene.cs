using BouncyBox.VorpalEngine.DebuggingGame.States.Game;
using BouncyBox.VorpalEngine.DebuggingGame.States.Render;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Game;

namespace BouncyBox.VorpalEngine.DebuggingGame.Scenes.Root
{
    public class RootScene : Scene
    {
        private readonly IGameStateManager<GameState> _gameStateManager;

        public RootScene(IInterfaces interfaces, IGameStateManager<GameState> gameStateManager, IEntityManager<GameState, RenderState> entityManager)
            : base(interfaces, entityManager, SceneKey.Root)
        {
            _gameStateManager = gameStateManager;

            AddUpdaters(new RootUpdater(interfaces, gameStateManager));
            AddRenderers(new RootRenderer(interfaces));
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