namespace BouncyBox.VorpalEngine.Engine.Threads
{
    /// <summary>The type of process thread.</summary>
    public enum ProcessThread
    {
        /// <summary>The main thread is used when the process spawns and is also used by the Windows message loop implementation.</summary>
        Main,

        /// <summary>This thread is used to update the game state and to get render delegates.</summary>
        Update,

        /// <summary>This thread is used to initialize and release update resources.</summary>
        UpdateResources,

        /// <summary>This thread is used to invoke render delegates.</summary>
        Render,

        /// <summary>This thread is used to initialize and release render resources.</summary>
        RenderResources
    }
}