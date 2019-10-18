namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>
    ///     <para>Represents an object that manages render states.</para>
    ///     <para>Render states are created after every update and represent just the information necessary to render the game world.</para>
    /// </summary>
    public interface IRenderStateManager<TRenderState>
        where TRenderState : class
    {
        /// <summary>
        ///     Provides a prepared render state.
        /// </summary>
        /// <param name="state">A prepared render state.</param>
        void ProvideNextRenderState(TRenderState state);

        /// <summary>
        ///     Gets the next render state.
        /// </summary>
        /// <returns>
        ///     Returns the next render state, if one exists; otherwise, returns null.
        /// </returns>
        TRenderState? GetNextRenderState();
    }
}