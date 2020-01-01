namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>Indicates whether to use the render state for rendering.</summary>
    public enum RenderStateResult
    {
        /// <summary>Use the render state when rendering the next frame.</summary>
        Use,

        /// <summary>Skip rendering the render state when rendering the next frame.</summary>
        Skip
    }
}