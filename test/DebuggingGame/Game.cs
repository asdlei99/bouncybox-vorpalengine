using BouncyBox.VorpalEngine.DebuggingGame.Scenes;
using BouncyBox.VorpalEngine.DebuggingGame.States.Game;
using BouncyBox.VorpalEngine.DebuggingGame.States.Render;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Bootstrap;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Scenes;

namespace BouncyBox.VorpalEngine.DebuggingGame
{
    public class Game : Game<GameState, RenderState, SceneKey>
    {
        public static readonly CommonGameSettings CommonGameSettings = new CommonGameSettings();

        public Game(
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