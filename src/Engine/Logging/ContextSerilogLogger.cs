using System;
using BouncyBox.Common.NetStandard21.Logging;
using Serilog.Events;

namespace BouncyBox.VorpalEngine.Engine.Logging
{
    /// <summary>
    ///     <para>A nested-context-aware Serilog logger.</para>
    ///     <para>The nested context string is built and prepended to the log message.</para>
    /// </summary>
    public class ContextSerilogLogger : ISerilogLogger
    {
        private readonly NestedContext _context;
        private readonly ISerilogLogger _serilogLogger;

        /// <summary>Initializes a new instance of the <see cref="ContextSerilogLogger" /> type.</summary>
        /// <param name="serilogLogger">An <see cref="ISerilogLogger" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        public ContextSerilogLogger(ISerilogLogger serilogLogger, NestedContext context)
        {
            _serilogLogger = serilogLogger;
            _context = context;
        }

        /// <inheritdoc />
        public void Log(LogEventLevel level, Exception? exception, string? messageTemplate = null, params object[] propertyValues)
        {
            _serilogLogger.Log(level, exception, FormatMessageTemplate(messageTemplate), propertyValues);
        }

        /// <inheritdoc />
        public void Log(LogEventLevel level, string? messageTemplate = null, params object[] propertyValues)
        {
            _serilogLogger.Log(level, FormatMessageTemplate(messageTemplate), propertyValues);
        }

        /// <inheritdoc />
        public void LogFatal(Exception? exception, string? messageTemplate = null, params object[] propertyValues)
        {
            _serilogLogger.LogFatal(exception, FormatMessageTemplate(messageTemplate), propertyValues);
        }

        /// <inheritdoc />
        public void LogFatal(string? messageTemplate = null, params object[] propertyValues)
        {
            _serilogLogger.LogFatal(FormatMessageTemplate(messageTemplate), propertyValues);
        }

        /// <inheritdoc />
        public void LogError(Exception? exception, string? messageTemplate = null, params object[] propertyValues)
        {
            _serilogLogger.LogError(exception, FormatMessageTemplate(messageTemplate), propertyValues);
        }

        /// <inheritdoc />
        public void LogError(string? messageTemplate = null, params object[] propertyValues)
        {
            _serilogLogger.LogError(FormatMessageTemplate(messageTemplate), propertyValues);
        }

        /// <inheritdoc />
        public void LogWarning(Exception? exception, string? messageTemplate = null, params object[] propertyValues)
        {
            _serilogLogger.LogWarning(exception, FormatMessageTemplate(messageTemplate), propertyValues);
        }

        /// <inheritdoc />
        public void LogWarning(string? messageTemplate = null, params object[] propertyValues)
        {
            _serilogLogger.LogWarning(FormatMessageTemplate(messageTemplate), propertyValues);
        }

        /// <inheritdoc />
        public void LogInformation(Exception? exception, string? messageTemplate = null, params object[] propertyValues)
        {
            _serilogLogger.LogInformation(exception, FormatMessageTemplate(messageTemplate), propertyValues);
        }

        /// <inheritdoc />
        public void LogInformation(string? messageTemplate = null, params object[] propertyValues)
        {
            _serilogLogger.LogInformation(FormatMessageTemplate(messageTemplate), propertyValues);
        }

        /// <inheritdoc />
        public void LogDebug(Exception? exception, string? messageTemplate = null, params object[] propertyValues)
        {
            _serilogLogger.LogDebug(exception, FormatMessageTemplate(messageTemplate), propertyValues);
        }

        /// <inheritdoc />
        public void LogDebug(string? messageTemplate = null, params object[] propertyValues)
        {
            _serilogLogger.LogDebug(FormatMessageTemplate(messageTemplate), propertyValues);
        }

        /// <inheritdoc />
        public void LogVerbose(Exception? exception, string? messageTemplate = null, params object[] propertyValues)
        {
            _serilogLogger.LogVerbose(exception, FormatMessageTemplate(messageTemplate), propertyValues);
        }

        /// <inheritdoc />
        public void LogVerbose(string? messageTemplate = null, params object[] propertyValues)
        {
            _serilogLogger.LogVerbose(FormatMessageTemplate(messageTemplate), propertyValues);
        }

        /// <inheritdoc />
        public void CloseAndFlush()
        {
            _serilogLogger.CloseAndFlush();
        }

        /// <summary>Formats the message template to include a prepended nested context string.</summary>
        /// <param name="messageTemplate">A Serilog message template.</param>
        /// <returns>Returns the new message template.</returns>
        private string FormatMessageTemplate(string? messageTemplate)
        {
            return $"[{_context.BuildString()}] {messageTemplate}";
        }
    }
}