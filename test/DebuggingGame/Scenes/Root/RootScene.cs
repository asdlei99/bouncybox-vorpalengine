using BouncyBox.VorpalEngine.DebuggingGame.States.Game;
using BouncyBox.VorpalEngine.Engine;

namespace BouncyBox.VorpalEngine.DebuggingGame.Scenes.Root
{
    public class RootScene : Scene
    {
        public RootScene(IInterfaces interfaces) : base(interfaces, SceneKey.Root)
        {
            AddUpdater(new RootUpdater(interfaces));
            AddRenderer(new RootRenderer(interfaces));
        }

        protected override void OnLoad(GameState gameState)
        {
            gameState.SceneStates.Root ??= new RootSceneGameState();
        }

        protected override void OnUnload(GameState gameState)
        {
            gameState.SceneStates.Root = null;
        }
    }
}