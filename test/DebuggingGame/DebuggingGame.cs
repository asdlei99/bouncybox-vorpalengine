using BouncyBox.VorpalEngine.DebuggingGame.States.Game;
using BouncyBox.VorpalEngine.DebuggingGame.States.Render;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Bootstrap;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Scenes;

namespace BouncyBox.VorpalEngine.DebuggingGame
{
    public class DebuggingGame : Game<GameState, RenderState, SceneKey>
    {
        public static readonly CommonGameSettings CommonGameSettings = new CommonGameSettings();

        public DebuggingGame(
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