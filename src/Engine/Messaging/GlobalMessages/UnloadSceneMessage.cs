using System;

namespace BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages
{
    /// <summary>
    ///     A global message requesting a scene be unloaded.
    /// </summary>
    public struct UnloadSceneMessage<TSceneKey> : IGlobalMessage
        where TSceneKey : struct, Enum
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UnloadSceneMessage{TSceneKey}" /> type.
        /// </summary>
        /// <param name="sceneKey">The key of the scene to unload.</param>
        public UnloadSceneMessage(TSceneKey sceneKey)
        {
            SceneKey = sceneKey;
        }

        /// <summary>
        ///     Gets the key of the scene to unload.
        /// </summary>
        public TSceneKey SceneKey { get; }
    }
}