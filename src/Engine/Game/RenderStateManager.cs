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
        private readonly IThreadManager _threadManager;
        private TRenderState? _nextState;

        /// <summary>Initializes a new instance of the <see cref="RenderStateManager{TRenderState}" /> type.</summary>
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

            Interlocked.Exchange(ref _nextState, state);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        public TRenderState? GetNextRenderState()
        {
            _threadManager.VerifyProcessThread(ProcessThread.Render);

            return Interlocked.Exchange(ref _nextState, null);
        }
    }
}