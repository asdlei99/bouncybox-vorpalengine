namespace BouncyBox.VorpalEngine.Engine.Threads
{
    /// <summary>The type of process thread.</summary>
    public enum EngineThread
    {
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