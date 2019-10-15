using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace BouncyBox.VorpalEngine.Engine
{
    /// <summary>
    ///     <para>A nested context that has many uses, most prominently for log messages.</para>
    ///     <para>
    ///         As nested contexts are passed further down the call stack, other objects may "push" their own context onto the nested
    ///         context, creating a "bread crumb" trail that can make log messages more useful.
    ///     </para>
    ///     <para>
    ///         Objects should mostly call <see cref="CopyAndPush(BouncyBox.VorpalEngine.Engine.NestedContext)" /> rather than
    ///         <see cref="Push(BouncyBox.VorpalEngine.Engine.NestedContext)" /> since calling
    ///         <see cref="Push(BouncyBox.VorpalEngine.Engine.NestedContext)" /> modifies the caller's nested context.
    ///     </para>
    /// </summary>
    [DebuggerStepThrough]
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class NestedContext : IEnumerable<string>
    {
        private readonly List<string> _contexts = new List<string>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="NestedContext" /> type.
        /// </summary>
        private NestedContext()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NestedContext" /> type.
        /// </summary>
        /// <param name="context">A context.</param>
        public NestedContext(string context)
        {
            Push(context);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NestedContext" /> type.
        /// </summary>
        /// <param name="context">A context type.</param>
        /// <param name="fullTypeName">A value that determines whether the full or short type name is used for the context.</param>
        public NestedContext(Type context, bool fullTypeName = false)
        {
            Push(context, fullTypeName);
        }

        /// <summary>
        ///     Gets a value indicating whether the context is empty.
        /// </summary>
        public bool IsEmpty => _contexts.Count == 0;

        private string DebuggerDisplay => $"Context = {BuildString()}";

        /// <inheritdoc />
        public IEnumerator<string> GetEnumerator()
        {
            return _contexts.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Pushes a context onto the list.
        /// </summary>
        /// <param name="context">A context.</param>
        public void Push(string context)
        {
            _contexts.Add(context);
        }

        /// <summary>
        ///     Pushes a context onto the list.
        /// </summary>
        /// <param name="context">A context type.</param>
        /// <param name="fullTypeName">A value that determines whether the full or short type name is used for the context.</param>
        public void Push(Type context, bool fullTypeName = false)
        {
            _contexts.Add((fullTypeName ? context.FullName : context.Name) ?? "Unknown type");
        }

        /// <summary>
        ///     Pushes another nested context onto the list.
        /// </summary>
        public void Push(NestedContext context)
        {
            _contexts.AddRange(context._contexts);
        }

        /// <summary>
        ///     Copies the nested context to a new nested context.
        /// </summary>
        /// <returns>Returns the new nested context.</returns>
        public NestedContext Copy()
        {
            var nestedContext = new NestedContext();

            nestedContext._contexts.AddRange(_contexts);

            return nestedContext;
        }

        /// <summary>
        ///     Copies the nested context to a new nested context, then pushes the specified context.
        /// </summary>
        /// <param name="context">A context.</param>
        /// <returns>Returns the new nested context.</returns>
        public NestedContext CopyAndPush(string context)
        {
            NestedContext nestedContext = Copy();

            nestedContext.Push(context);

            return nestedContext;
        }

        /// <summary>
        ///     Copies the nested context to a new nested context, then pushes the specified context.
        /// </summary>
        /// <param name="context">A context type.</param>
        /// <param name="fullTypeName">A value that determines whether the full or short type name is used for the context.</param>
        /// <returns>Returns the new nested context.</returns>
        public NestedContext CopyAndPush(Type context, bool fullTypeName = false)
        {
            NestedContext nestedContext = Copy();

            nestedContext.Push(context, fullTypeName);

            return nestedContext;
        }

        /// <summary>
        ///     Copies the nested context to a new nested context, then pushes the specified nested context.
        /// </summary>
        /// <param name="context">A nested context.</param>
        /// <returns>Returns the new nested context.</returns>
        public NestedContext CopyAndPush(NestedContext context)
        {
            NestedContext nestedContext = Copy();

            nestedContext.Push(context);

            return this;
        }

        /// <summary>
        ///     Builds a string from the list of contexts.
        /// </summary>
        /// <param name="separator">The separator that separates each context in the string.</param>
        /// <param name="unknownContext">The phrase to use when the context is unknown.</param>
        /// <returns>Returns the concatenated contexts.</returns>
        public string BuildString(string separator = "->", string unknownContext = "UnknownContext")
        {
            return _contexts.Count == 0 ? unknownContext : string.Join(separator, _contexts);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return DebuggerDisplay;
        }

        /// <summary>
        ///     Creates a new nested context with no context.
        /// </summary>
        /// <returns>Returns the new nested context.</returns>
        public static NestedContext None()
        {
            return new NestedContext();
        }
    }
}