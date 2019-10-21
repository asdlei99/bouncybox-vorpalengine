using System.Threading;
using BouncyBox.VorpalEngine.Engine.DirectX;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>Represents an entity that can render itself.</summary>
    public interface IRenderingEntity : IEntity
    {
        /// <summary>
        ///     Gets the entity's position in render order, which is determined by sorting all entities' render orders in ascending
        ///     order.
        /// </summary>
        uint RenderOrder { get; }

        /// <summary>Initializes render resources.</summary>
        /// <param name="resources">DirectX resources.</param>
        void InitializeRenderResources(DirectXResources resources);

        /// <summary>Resizes render resources to account for the new render window client size.</summary>
        /// <param name="clientSize">The size of the render window's client area.</param>
        void ResizeRenderResources(D2D_SIZE_U clientSize);

        /// <summary>Releases render resources created by this entity.</summary>
        void ReleaseRenderResources();

        /// <summary>Renders the entity.</summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>Returns the result of the render attempt.</returns>
        EntityRenderResult Render(CancellationToken cancellationToken);
    }
}