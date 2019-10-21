using System;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>Represents an entity.</summary>
    public interface IEntity : IDisposable
    {
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