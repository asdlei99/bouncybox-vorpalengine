using BouncyBox.VorpalEngine.Engine;

namespace BouncyBox.VorpalEngine.SampleGame.Scenes.Root
{
    public class RootScene : Scene
    {
        public RootScene(IInterfaces interfaces) : base(interfaces, SceneKey.Root)
        {
            AddUpdater(new LoadingUpdater(interfaces));
            AddRenderer(new LoadingRenderer(interfaces));
        }
    }
}