using System;
using BouncyBox.VorpalEngine.Common;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Threads;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>Base class for all entities.</summary>
    public abstract class Entity : IEntity
    {
        private bool _isDisposed;

        /// <summary>Initializes a new instance of the <see cref="Entity" /></summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        protected Entity(IInterfaces interfaces, NestedContext context)
        {
            Interfaces = interfaces;
            Context = context;

            GlobalMessagePublisherSubscriber = ConcurrentMessagePublisherSubscriber<IGlobalMessage>.Create(interfaces, context);
        }

        /// <summary>Initializes a new instance of the <see cref="Entity" /></summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        protected Entity(IInterfaces interfaces) : this(interfaces, NestedContext.None())
        {
        }

        /// <summary>Gets the <see cref="IInterfaces" /> implementation.</summary>
        protected IInterfaces Interfaces { get; }

        /// <summary>Gets the nested context.</summary>
        protected NestedContext Context { get; }

        /// <summary>
        ///     <para>Gets the global message publisher/subscriber.</para>
        ///     <para>Use the global message queue to publish or subscribe to messages intended to be processed globally.</para>
        ///     <para>Do not use the global message queue to send update-queue-specific messages.</para>
        /// </summary>
        protected ConcurrentMessagePublisherSubscriber<IGlobalMessage> GlobalMessagePublisherSubscriber { get; }

        /// <summary>Gets a value indicating if the entity is neither paused nor suspended.</summary>
        protected bool IsRunning => !IsPaused && !IsSuspended;

        /// <summary>Gets a value indicating if the entity is paused.</summary>
        protected bool IsPaused { get; private set; }

        /// <summary>Gets a value indicating if the entity is suspended.</summary>
        protected bool IsSuspended { get; private set; }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="Threads.ProcessThread.Update" /> thread.
        /// </exception>
        public void Pause()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            if (IsPaused)
            {
                return;
            }

            IsPaused = true;

            OnPause();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Update" /> thread.
        /// </exception>
        public void Unpause()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            if (!IsPaused)
            {
                return;
            }

            IsPaused = false;

            OnUnpause();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Update" /> thread.
        /// </exception>
        public void Suspend()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            if (IsSuspended)
            {
                return;
            }

            IsSuspended = true;

            OnSuspend();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Update" /> thread.
        /// </exception>
        public void Resume()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            if (!IsSuspended)
            {
                return;
            }

            IsSuspended = false;

            OnResume();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Update" /> thread.
        /// </exception>
        public void Dispose()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="Pause" />
        protected virtual void OnPause()
        {
        }

        /// <inheritdoc cref="Unpause" />
        protected virtual void OnUnpause()
        {
        }

        /// <inheritdoc cref="Suspend" />
        protected virtual void OnSuspend()
        {
        }

        /// <inheritdoc cref="Resume" />
        protected virtual void OnResume()
        {
        }

        /// <summary>Disposes managed and unmanaged resources.</summary>
        /// <param name="disposing">A value indicating whether managed resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            DisposeHelper.Dispose(GlobalMessagePublisherSubscriber.Dispose, ref _isDisposed);
        }
    }
}