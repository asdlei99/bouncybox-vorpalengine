namespace BouncyBox.VorpalEngine.Engine.DirectX
{
    /// <summary>
    ///     The result of a call to <see cref="DirectXResourceManager.Render" />.
    /// </summary>
    internal enum RenderResult
    {
        /// <summary>
        ///     A frame was rendered.
        /// </summary>
        FrameRendered,

        /// <summary>
        ///     A frame was not rendered, likely due to no new render state being available.
        /// </summary>
        FrameSkipped,

        /// <summary>
        ///     Core DirectX resources were reinitialized, likely due to render target invalidation.
        /// </summary>
        Reinitialized
    }
}