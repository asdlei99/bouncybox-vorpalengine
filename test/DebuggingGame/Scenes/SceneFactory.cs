using System;
using BouncyBox.VorpalEngine.DebuggingGame.Scenes.Root;
using BouncyBox.VorpalEngine.DebuggingGame.States.Game;
using BouncyBox.VorpalEngine.DebuggingGame.States.Render;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Scenes;

namespace BouncyBox.VorpalEngine.DebuggingGame.Scenes
{
    public class SceneFactory : ISceneFactory<GameState, RenderState, SceneKey>
    {
        private readonly IEntityManager<GameState, RenderState> _entityManager;
        private readonly IGameStateManager<GameState> _gameStateManager;
        private readonly IInterfaces _interfaces;

        public SceneFactory(IInterfaces interfaces, IGameStateManager<GameState> gameStateManager, IEntityManager<GameState, RenderState> entityManager)
        {
            _interfaces = interfaces;
            _gameStateManager = gameStateManager;
            _entityManager = entityManager;
        }

        public IScene<SceneKey> Create(SceneKey sceneKey)
        {
            return sceneKey switch
            {
                SceneKey.Root => new RootScene(_interfaces, _gameStateManager, _entityManager),
                _ => throw new ArgumentOutOfRangeException(nameof(sceneKey), sceneKey, null)
            };
        }
    }
}