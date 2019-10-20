using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Entities;

namespace BouncyBox.VorpalEngine.SampleGame.Scenes.Title
{
    public class TitleScene : Scene
    {
        public TitleScene(IInterfaces interfaces, IEntityManager<GameState> entityManager) : base(interfaces, entityManager, SceneKey.Title)
        {
            AddEntities(new TitleEntity(interfaces));
        }
    }
}