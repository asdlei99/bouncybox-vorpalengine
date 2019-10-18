using System;
using System.Threading;
using BouncyBox.VorpalEngine.Engine.Threads;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>
    ///     <para>Manages render states.</para>
    ///     <para>Render states are created after every update and represent just the information necessary to render the game world.</para>
    /// </summary>
    public class RenderStateManager<TRenderState> : IRenderStateManager<TRenderState>
        where TRenderState : class
    {
        private readonly object _lockObject = new object();
        private readonly ManualResetEventSlim _manualResetEvent = new ManualResetEventSlim();
        private readonly IThreadManager _threadManager;
        private bool _isDisposed;
        private TRenderState? _nextState;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RenderStateManager{TRenderState}" /> type.
        /// </summary>
        /// <param name="threadManager">An <see cref="IThreadManager" /> implemenetation.</param>
        public RenderStateManager(IThreadManager threadManager)
        {
            _threadManager = threadManager;
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        public void ProvideNextRenderState(TRenderState state)
        {
            _threadManager.VerifyProcessThread(ProcessThread.Update);

            lock (_lockObject)
            {
                _nextState = state;
            }

            _manualResetEvent.Set();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        public TRenderState? GetRenderStateForRendering(CancellationToken cancellationToken = default)
        {
            _threadManager.VerifyProcessThread(ProcessThread.Render);

            try
            {
                // Wait for the next render state to be provided
                _manualResetEvent.Wait(cancellationToken);

                // Reset the MRE so that the next attempt to get a render state for rendering will block if the next render state wasn't provided
                _manualResetEvent.Reset();
            }
            catch (OperationCanceledException)
            {
                // The render loop was canceled
                return null;
            }

            lock (_lockObject)
            {
                // It doesn't matter if _nextState was updated several times since the Wait() call returned
                return _nextState;
            }
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the main thread.</exception>
        public void Dispose()
        {
            DisposeHelper.Dispose(_manualResetEvent.Dispose, ref _isDisposed, _threadManager, ProcessThread.Main);
        }
    }
}