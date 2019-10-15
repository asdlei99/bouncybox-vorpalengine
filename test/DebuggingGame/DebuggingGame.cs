using BouncyBox.VorpalEngine.DebuggingGame.States.Render;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Bootstrap;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Scenes;

namespace BouncyBox.VorpalEngine.DebuggingGame
{
    public class DebuggingGame : Game<RenderState, SceneKey>
    {
        public static readonly CommonGameSettings CommonGameSettings = new CommonGameSettings();

        public DebuggingGame(
            IInterfaces interfaces,
            IGameExecutionStateManager gameExecutionStateManager,
            IRenderStateManager<RenderState> renderStateManager,
            ISceneManager<RenderState> sceneManager,
            ProgramOptions programOptions)
            : base(interfaces, gameExecutionStateManager, renderStateManager, sceneManager, programOptions)
        {
        }
    }
}