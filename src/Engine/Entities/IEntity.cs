using System;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>An entity.</summary>
    public interface IEntity : IDisposable
    {
        /// <summary>Gets a value that determines the order in which to process this entity when compared to other entities' orders.</summary>
        uint Order { get; }

        /// <summary>Allows the entity to respond to the game execution state being paused.</summary>
        void Pause();

        /// <summary>Allows the entity to respond to the game execution state being unpaused.</summary>
        void Unpause();

        /// <summary>Allows the entity to respond to the game execution state being suspended.</summary>
        void Suspend();

        /// <summary>Allows the entity to respond to the game execution state being resumed.</summary>
        void Resume();
    }
}