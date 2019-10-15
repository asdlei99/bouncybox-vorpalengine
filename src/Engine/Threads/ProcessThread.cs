﻿namespace BouncyBox.VorpalEngine.Engine.Threads
{
    /// <summary>
    ///     The type of process thread.
    /// </summary>
    public enum ProcessThread
    {
        /// <summary>
        ///     The main thread is used when the process spawns, and is also used by the Windows message loop implementation.
        /// </summary>
        Main,

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