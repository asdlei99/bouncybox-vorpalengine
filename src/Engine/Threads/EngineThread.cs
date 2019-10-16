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
        ///     The DirectX thread is used to initialize and release DirectX resources.
        /// </summary>
        DirectX,

        /// <summary>
        ///     The render thread is used to render render states.
        /// </summary>
        Render
    }
}