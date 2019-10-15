using BouncyBox.VorpalEngine.Engine;

namespace BouncyBox.VorpalEngine.SampleGame.Scenes.Title
{
    public class TitleScene : Scene
    {
        public TitleScene(IInterfaces interfaces) : base(interfaces, SceneKey.Title)
        {
            AddRenderer(new TitleRenderer(interfaces));
        }
    }
}