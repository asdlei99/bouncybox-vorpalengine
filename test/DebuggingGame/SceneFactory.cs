using System;
using BouncyBox.VorpalEngine.DebuggingGame.Scenes.Root;
using BouncyBox.VorpalEngine.DebuggingGame.States.Game;
using BouncyBox.VorpalEngine.DebuggingGame.States.Render;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Scenes;

namespace BouncyBox.VorpalEngine.DebuggingGame
{
    public class SceneFactory : ISceneFactory<GameState, RenderState, SceneKey>
    {
        private readonly IInterfaces _interfaces;

        public SceneFactory(IInterfaces interfaces)
        {
            _interfaces = interfaces;
        }

        public IScene<GameState, RenderState, SceneKey> Create(SceneKey sceneKey)
        {
            return sceneKey switch
            {
                SceneKey.Root => new RootScene(_interfaces),
                _ => throw new ArgumentOutOfRangeException(nameof(sceneKey), sceneKey, null)
            };
        }
    }
}