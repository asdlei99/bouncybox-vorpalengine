namespace BouncyBox.VorpalEngine.Engine.Threads
{
    /// <summary>
    ///     The type of process thread.
    /// </summary>
    public enum EngineThread
    {
        /// <summary>
        ///     This thread is used to update the game state and to prepare render states.
        /// </summary>
        Update,

        /// <summary>
        ///     This thread is used to initialize and release updater resources.
        /// </summary>
        UpdaterResources,

        /// <summary>
        ///     This thread is used to render render states.
        /// </summary>
        Render,

        /// <summary>
        ///     This thread is used to initialize and release renderer resources.
        /// </summary>
        RendererResources
    }
}