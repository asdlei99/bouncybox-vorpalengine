using System.Drawing;
using System.Threading;

namespace BouncyBox.VorpalEngine.Engine.Entities.Updaters
{
    /// <summary>Represents an updater's capability to use resources.</summary>
    public interface IUpdater : IEntity
    {
        /// <summary>Initializes resources.</summary>
        void InitializeResources();

        /// <summary>Resizes resources to account for the new render window client size.</summary>
        /// <param name="clientSize">The size of the render window's client area.</param>
        void ResizeResources(Size clientSize);

        /// <summary>Releases resources created by this updater.</summary>
        void ReleaseResources();
    }

    /// <summary>Represents an entity that updates the game state.</summary>
    public interface IUpdater<in TRenderState> : IUpdater
        where TRenderState : class
    {
        /// <summary>Updates the game state.</summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        void UpdateGameState(CancellationToken cancellationToken);

        /// <summary>Prepares a render state for rendering.</summary>
        /// <param name="renderState">A render state.</param>
        void PrepareRenderState(TRenderState renderState);
    }
}