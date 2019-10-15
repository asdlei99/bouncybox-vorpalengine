namespace BouncyBox.VorpalEngine.Engine.Threads
{
    /// <summary>
    ///     The type of process thread.
    /// </summary>
    public enum EngineThread
    {
        /// <summary>
        ///     The update thread is used to update the game state and to prepare render states.
        /// </summary>
        Update,

        /// <summary>
        ///     The render thread is used to render entities.
        /// </summary>
        Render
    }
}