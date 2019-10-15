using System;
using System.Threading;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>
    ///     <para>Represents an object that manages render states.</para>
    ///     <para>Render states are created after every update and represent just the information necessary to render the game world.</para>
    /// </summary>
    public interface IRenderStateManager<TRenderState> : IDisposable
        where TRenderState : class
    {
        /// <summary>
        ///     Provides a prepared render state.
        /// </summary>
        /// <param name="state">A prepared render state.</param>
        void ProvideNextRenderState(TRenderState state);

        /// <summary>
        ///     Gets a prepared render state for rendering.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>
        ///     Returns a prepared render state if one exists and is newer than the previously retrieved render state; otherwise,
        ///     returns null.
        /// </returns>
        TRenderState? GetRenderStateForRendering(CancellationToken cancellationToken = default);
    }
}