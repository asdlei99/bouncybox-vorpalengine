namespace BouncyBox.VorpalEngine.Engine.Threads
{
    /// <summary>The type of engine thread.</summary>
    public enum EngineThread
    {
        /// <summary>This thread is used to update the game state and generate render states.</summary>
        Update,

        /// <summary>This thread is used to initialize and release render resources, and to render entities.</summary>
        Render
    }
}