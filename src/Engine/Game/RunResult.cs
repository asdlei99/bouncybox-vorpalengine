namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>The result of a game run.</summary>
    public enum RunResult
    {
        /// <summary>The game was run successfully.</summary>
        Success,

        /// <summary>An unhandled exception occurred.</summary>
        UnhandledException,

        /// <summary>Command line arguments were invalid.</summary>
        InvalidCommandLineArguments
    }
}