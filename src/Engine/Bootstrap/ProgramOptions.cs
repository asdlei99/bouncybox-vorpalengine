using CommandLine;
using Serilog.Events;

namespace BouncyBox.VorpalEngine.Engine.Bootstrap
{
    /// <summary>
    ///     Encapsulates common command line parameters.
    /// </summary>
    public class ProgramOptions
    {
        /// <summary>
        ///     Gets or sets a value that indicates whether logging is enabled.
        /// </summary>
        [Option("loggingenabled", HelpText = "Determines whether logging is enabled.", Default = true)]
        public bool IsLoggingEnabled { get; set; }

        /// <summary>
        ///     Gets or sets a value that controls the minimum log level to output.
        /// </summary>
        [Option("minimumloglevel", HelpText = "The minimum log level of messages to log.", Default = LogEventLevel.Information)]
        public LogEventLevel MinimumLogLevel { get; set; }

        /// <summary>
        ///     Gets or sets a value that indicates whether Windows messages sent to the render window are logged.
        /// </summary>
        [Option("logwindowsmessages", HelpText = "Determines whether to log Windows messages sent to the render window2.", Default = false)]
        public bool LogWindowsMessages { get; set; }
    }
}