using System;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Scenes;
using BouncyBox.VorpalEngine.SampleGame.Scenes.Root;
using BouncyBox.VorpalEngine.SampleGame.Scenes.Title;
using BouncyBox.VorpalEngine.SampleGame.States.Game;
using BouncyBox.VorpalEngine.SampleGame.States.Render;

namespace BouncyBox.VorpalEngine.SampleGame
{
    public class SceneFactory : ISceneFactory<GameState, RenderState, SceneKey>
    {
        private readonly IEntityManager<GameState, RenderState> _entityManager;
        private readonly IInterfaces _interfaces;

        public SceneFactory(IInterfaces interfaces, IEntityManager<GameState, RenderState> entityManager)
        {
            _interfaces = interfaces;
            _entityManager = entityManager;
        }

        public IScene<SceneKey> Create(SceneKey sceneKey)
        {
            return sceneKey switch
            {
                SceneKey.Root => (IScene<SceneKey>)new RootScene(_interfaces, _entityManager),
                SceneKey.Title => new TitleScene(_interfaces, _entityManager),
                _ => throw new ArgumentOutOfRangeException(nameof(sceneKey), sceneKey, null)
            };
        }
    }
}