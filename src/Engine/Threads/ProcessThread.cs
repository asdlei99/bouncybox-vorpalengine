namespace BouncyBox.VorpalEngine.Engine.Threads
{
    /// <summary>The type of process thread.</summary>
    public enum ProcessThread
    {
        /// <summary>This thread is spawned by the operating system. The Windows message loop implementation runs on it.</summary>
        Main,

        /// <summary>This thread is used to update the game state and generate render states.</summary>
        Update,

        /// <summary>This thread is used to initialize and release render resources, and to render entities.</summary>
        Render
    }
}