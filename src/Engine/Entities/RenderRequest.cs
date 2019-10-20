using System;
using System.Threading;
using BouncyBox.VorpalEngine.Engine.DirectX;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>A request to render an entity.</summary>
    public struct RenderRequest
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RenderRequest" /> type.
        /// </summary>
        /// <param name="renderOrder">The entity's render order.</param>
        /// <param name="renderDelegate">The delegate that will render the entity when invoked.</param>
        public RenderRequest(uint renderOrder, Action<DirectXResources, CancellationToken> renderDelegate)
        {
            RenderOrder = renderOrder;
            RenderDelegate = renderDelegate;
        }

        /// <summary>Gets the render order of the entity associated with the render request.</summary>
        public uint RenderOrder { get; }

        /// <summary>Gets delegate that will render the entity when invoked.</summary>
        public Action<DirectXResources, CancellationToken> RenderDelegate { get; }
    }
}