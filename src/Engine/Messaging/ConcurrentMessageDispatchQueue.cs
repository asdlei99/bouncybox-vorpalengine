using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using BouncyBox.Common.NetStandard21.Logging;
using BouncyBox.VorpalEngine.Engine.Logging;

namespace BouncyBox.VorpalEngine.Engine.Messaging
{
    /// <summary>
    ///     A queue that can receive dispatched global messages with the intent of potentially processing those messages on a
    ///     different thread.
    /// </summary>
    public class ConcurrentMessageDispatchQueue<TMessageBase> : IDisposable
        where TMessageBase : IGlobalMessage
    {
        private readonly NestedContext _context;
        private readonly Dictionary<Type, Action<TMessageBase>> _handlersByMessageType = new Dictionary<Type, Action<TMessageBase>>();
        private readonly ConcurrentQueue<TMessageBase> _queue = new ConcurrentQueue<TMessageBase>();
        private readonly ContextSerilogLogger _serilogLogger;
        private bool _isDisposed;

        /// <summary>Initializes a new instance of the <see cref="ConcurrentMessageDispatchQueue{TMessageBase}" /> type.</summary>
        /// <param name="serilogLogger">An <see cref="ISerilogLogger" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        public ConcurrentMessageDispatchQueue(ISerilogLogger serilogLogger, NestedContext context)
        {
            _context = context.CopyAndPush(nameof(ConcurrentMessageDispatchQueue<TMessageBase>));
            _serilogLogger = new ContextSerilogLogger(serilogLogger, _context);
        }

        /// <summary>Initializes a new instance of the <see cref="ConcurrentMessageDispatchQueue{TMessageBase}" /> type.</summary>
        /// <param name="serilogLogger">An <see cref="ISerilogLogger" /> implementation.</param>
        public ConcurrentMessageDispatchQueue(ISerilogLogger serilogLogger)
            : this(serilogLogger, NestedContext.None())
        {
        }

        /// <inheritdoc />
        public void Dispose()
        {
            DisposeHelper.Dispose(
                () =>
                {
                    _queue.Clear();
                    _handlersByMessageType.Clear();
                },
                ref _isDisposed);
        }

        /// <summary>Invokes the specified delegate when a dispatched message of the specified type is handled.</summary>
        /// <param name="handlerDelegate">A delegate that is invoked when a dispatched message of the specified type is handled.</param>
        /// <returns>Returns the concurrent message dispatch queue.</returns>
        public ConcurrentMessageDispatchQueue<TMessageBase> Handle<TMessage>(Action<TMessage> handlerDelegate)
            where TMessage : TMessageBase
        {
            Type messageType = typeof(TMessage);

            _handlersByMessageType.Add(messageType, a => handlerDelegate((TMessage)a));

            bool shouldLogMessage = MessageLogFilter.ShouldLogMessageTypeDelegate(messageType);

            if (shouldLogMessage)
            {
                _serilogLogger.LogDebug("{Context} will handle {MessageType}", _context.BuildString(), messageType.Name);
            }

            return this;
        }

        /// <summary>
        ///     Dispatches a message to the queue. The message will be processed with a subsequent call to
        ///     <see cref="HandleDispatched" />.
        /// </summary>
        /// <param name="message">The message to dispatch.</param>
        /// <returns>Returns the concurrent message dispatch queue.</returns>
        public ConcurrentMessageDispatchQueue<TMessageBase> Dispatch<TMessage>(TMessage message)
            where TMessage : TMessageBase
        {
            string messageType = message.GetType().Name;
            // Determine whether to log the message before and after publishing
            bool shouldLogMessage = message.ShouldLog();

            if (shouldLogMessage)
            {
                _serilogLogger.LogDebug("Dispatching {MessageType}", messageType);
            }

            // Enqueue the message in the message dispatch queue
            _queue.Enqueue(message);

            if (shouldLogMessage)
            {
                _serilogLogger.LogVerbose("Dispatched {MessageType}", messageType);
            }

            return this;
        }

        /// <summary>Dequeues dispatched messages, calling the appropriate handler for each message.</summary>
        public void HandleDispatched()
        {
            while (_queue.TryDequeue(out TMessageBase message))
            {
                // Determine whether to log the message before and after publishing
                bool shouldLogMessage = message.ShouldLog();

                if (shouldLogMessage)
                {
                    _serilogLogger.LogDebug("Handling {MessageType}", message.GetType().Name);
                }

                _handlersByMessageType[message.GetType()](message);

                if (shouldLogMessage)
                {
                    _serilogLogger.LogVerbose("Handled {MessageType}", message.GetType().Name);
                }
            }
        }
    }
}