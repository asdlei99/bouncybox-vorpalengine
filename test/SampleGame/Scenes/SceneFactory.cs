using System;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Scenes;
using BouncyBox.VorpalEngine.SampleGame.Scenes.Loading;
using BouncyBox.VorpalEngine.SampleGame.Scenes.Title;

namespace BouncyBox.VorpalEngine.SampleGame.Scenes
{
    public class SceneFactory : ISceneFactory<SceneKey>
    {
        private readonly IEntityManager<GameState> _entityManager;
        private readonly IGameStateManager<GameState> _gameStateManager;
        private readonly IInterfaces _interfaces;

        public SceneFactory(IInterfaces interfaces, IGameStateManager<GameState> gameStateManager, IEntityManager<GameState> entityManager)
        {
            _interfaces = interfaces;
            _gameStateManager = gameStateManager;
            _entityManager = entityManager;
        }

        public IScene<SceneKey> Create(SceneKey sceneKey)
        {
            return sceneKey switch
            {
                SceneKey.Loading => (IScene<SceneKey>)new LoadingScene(_interfaces, _gameStateManager, _entityManager),
                SceneKey.Title => new TitleScene(_interfaces, _entityManager),
                _ => throw new ArgumentOutOfRangeException(nameof(sceneKey), sceneKey, null)
            };
        }
    }
}