using BouncyBox.VorpalEngine.DebuggingGame.States.Game;
using BouncyBox.VorpalEngine.DebuggingGame.States.Render;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Scenes;

namespace BouncyBox.VorpalEngine.DebuggingGame
{
    public abstract class Scene : Scene<GameState, RenderState, SceneKey>
    {
        protected Scene(IInterfaces interfaces, SceneKey key) : base(interfaces, key)
        {
        }
    }
}