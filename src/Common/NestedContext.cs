using System;
using System.Diagnostics;

namespace BouncyBox.VorpalEngine.Common
{
    /// <summary>
    ///     <para>A nested context that has many uses, most prominently for log messages.</para>
    ///     <para>
    ///         As nested contexts are passed further down the call stack, other objects may "push" their own context onto the nested
    ///         context, creating a "bread crumb" trail that can make log messages more useful.
    ///     </para>
    /// </summary>
    [DebuggerStepThrough]
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public readonly struct NestedContext
    {
        private readonly string _separator;

        /// <summary>Initializes a new instance of the <see cref="NestedContext" /> type.</summary>
        /// <param name="context">A context.</param>
        /// <param name="separator">The separator to use for all derived contexts.</param>
        public NestedContext(string? context = null, string separator = "->") : this("", context, separator)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="NestedContext" /> type.</summary>
        /// <param name="existingContext">An existing context.</param>
        /// <param name="newContext">The new context to push.</param>
        /// <param name="separator">The separator to use for all derived contexts.</param>
        private NestedContext(string? existingContext, string? newContext, string separator)
        {
            _separator = separator;

            Context = $"{existingContext}{(!string.IsNullOrEmpty(existingContext) && !string.IsNullOrEmpty(newContext) ? separator : "")}{newContext}";
        }

        /// <summary>Gets the context.</summary>
        public string? Context { get; }

        private string DebuggerDisplay => $"Context = {Context}";

        /// <summary>Returns a new nested context containing the current context and the new context.</summary>
        /// <param name="context">The new context to push.</param>
        /// <returns>Returns the new nested context.</returns>
        public NestedContext Push(string context)
        {
            return new NestedContext(Context, context, _separator);
        }

        /// <summary>Returns a new nested context containing the current context and a new context derived from a type.</summary>
        /// <param name="context">A context type.</param>
        /// <param name="fullTypeName">A value that determines whether the full or short type name is used for the context.</param>
        /// <returns>Returns the new nested context.</returns>
        public NestedContext Push(Type context, bool fullTypeName = false)
        {
            return Push((fullTypeName ? context.FullName : context.Name) ?? "Unknown type");
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return DebuggerDisplay;
        }

        /// <summary>Creates a new nested context with no context.</summary>
        /// <returns>Returns the new nested context.</returns>
        public static NestedContext None(string separator = "->")
        {
            return new NestedContext(null, separator);
        }
    }
}