using System;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Scenes;
using BouncyBox.VorpalEngine.SampleGame.Scenes.Root;
using BouncyBox.VorpalEngine.SampleGame.Scenes.Title;
using BouncyBox.VorpalEngine.SampleGame.States.Game;
using BouncyBox.VorpalEngine.SampleGame.States.Render;

namespace BouncyBox.VorpalEngine.SampleGame
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
                SceneKey.Root => (IScene<GameState, RenderState, SceneKey>)new RootScene(_interfaces),
                SceneKey.Title => new TitleScene(_interfaces),
                _ => throw new ArgumentOutOfRangeException(nameof(sceneKey), sceneKey, null)
            };
        }
    }
}