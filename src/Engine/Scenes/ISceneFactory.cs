using System;

namespace BouncyBox.VorpalEngine.Engine.Scenes
{
    /// <summary>Represents a factory that creates scenes.</summary>
    public interface ISceneFactory<in TGameState, in TRenderState, TSceneKey>
        where TGameState : class
        where TRenderState : class
        where TSceneKey : struct, Enum
    {
        /// <summary>Creates a scene for the given scene key.</summary>
        /// <param name="sceneKey">A scene key.</param>
        /// <returns>Returns the new scene.</returns>
        IScene<TSceneKey> Create(TSceneKey sceneKey);
    }
}