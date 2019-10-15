using System;

namespace BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages
{
    /// <summary>
    ///     A global message that requests a scene be loaded.
    /// </summary>
    public struct LoadSceneMessage<TSceneKey> : IGlobalMessage
        where TSceneKey : struct, Enum
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LoadSceneMessage{TSceneKey}" /> type.
        /// </summary>
        /// <param name="sceneKey">The key of the scene to load.</param>
        public LoadSceneMessage(TSceneKey sceneKey)
        {
            SceneKey = sceneKey;
        }

        /// <summary>
        ///     Gets the key of the scene to load.
        /// </summary>
        public TSceneKey SceneKey { get; }
    }
}