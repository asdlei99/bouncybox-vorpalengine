namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>
    ///     The result of a render attempt.
    /// </summary>
    public enum RenderResult
    {
        /// <summary>
        ///     A frame was rendered.
        /// </summary>
        FrameRendered,

        /// <summary>
        ///     The frame was skipped.
        /// </summary>
        FrameSkipped,

        /// <summary>
        ///     The render target needs to be recreated.
        /// </summary>
        RecreateTarget
    }
}