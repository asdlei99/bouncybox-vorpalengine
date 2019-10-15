using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Scenes;
using BouncyBox.VorpalEngine.SampleGame.States.Game;
using BouncyBox.VorpalEngine.SampleGame.States.Render;

namespace BouncyBox.VorpalEngine.SampleGame
{
    public abstract class Scene : Scene<GameState, RenderState, SceneKey>
    {
        protected Scene(IInterfaces interfaces, SceneKey key) : base(interfaces, key)
        {
        }
    }
}