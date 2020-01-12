using BouncyBox.VorpalEngine.DebuggingGame.Scenes;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Bootstrap;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Scenes;

namespace BouncyBox.VorpalEngine.DebuggingGame
{
    public class Game : Game<GameState, SceneKey>
    {
        public static readonly CommonGameSettings CommonGameSettings = new CommonGameSettings();

        public Game(
            IInterfaces interfaces,
            IGameExecutionStateManager gameExecutionStateManager,
            IEntityManager<GameState> entityManager,
            IDirectXResourceManager<GameState> directXResourceManager,
            ISceneManager sceneManager,
            ProgramOptions programOptions)
            : base(interfaces, gameExecutionStateManager, entityManager, directXResourceManager, sceneManager, programOptions)
        {
        }
    }
}