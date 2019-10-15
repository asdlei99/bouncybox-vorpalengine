using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Bootstrap;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Scenes;
using BouncyBox.VorpalEngine.SampleGame.States.Render;

namespace BouncyBox.VorpalEngine.SampleGame
{
    public class SampleGame : Game<RenderState, SceneKey>
    {
        public static readonly CommonGameSettings CommonGameSettings = new CommonGameSettings();

        public SampleGame(
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