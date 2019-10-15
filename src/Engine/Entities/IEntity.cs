using System;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>
    ///     An entity.
    /// </summary>
    public interface IEntity : IDisposable
    {
        /// <summary>
        ///     Gets a value that determines the order in which to process this entity when compared to other entities' orders.
        /// </summary>
        uint Order { get; }
    }
}