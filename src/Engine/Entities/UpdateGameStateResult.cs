namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>The result of an entity updating the game state.</summary>
    public enum UpdateGameStateResult
    {
        /// <summary>The entity is requesting a render.</summary>
        Render,

        /// <summary>The entity is not requesting a render.</summary>
        DoNotRender
    }
}