using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Bootstrap;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Scenes;
using BouncyBox.VorpalEngine.SampleGame.States.Game;
using BouncyBox.VorpalEngine.SampleGame.States.Render;

namespace BouncyBox.VorpalEngine.SampleGame
{
    public class SampleGame : Game<GameState, RenderState, SceneKey>
    {
        public static readonly CommonGameSettings CommonGameSettings = new CommonGameSettings();

        public SampleGame(
            IInterfaces interfaces,
            IGameExecutionStateManager gameExecutionStateManager,
            IEntityManager<GameState, RenderState> entityManager,
            ISceneManager sceneManager,
            ProgramOptions programOptions)
            : base(interfaces, gameExecutionStateManager, entityManager, sceneManager, programOptions)
        {
        }
    }
}