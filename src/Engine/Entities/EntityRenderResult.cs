namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>The result of an entity's render attempt.</summary>
    public enum EntityRenderResult
    {
        /// <summary>The entity was rendered.</summary>
        FrameRendered,

        /// <summary>The entity was skipped.</summary>
        FrameSkipped,
    }
}