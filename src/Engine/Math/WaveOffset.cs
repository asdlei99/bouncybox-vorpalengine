namespace BouncyBox.VorpalEngine.Engine.Math
{
    /// <summary>
    ///     An X-axis offset.
    /// </summary>
    public enum WaveOffset
    {
        /// <summary>
        ///     No offset.
        /// </summary>
        None,

        /// <summary>
        ///     Offset the X-axis such that Y is the trough of the wave when X is zero.
        /// </summary>
        Trough,

        /// <summary>
        ///     Offset the X-axis such that Y is the crest of the wave when X is zero.
        /// </summary>
        Crest
    }
}