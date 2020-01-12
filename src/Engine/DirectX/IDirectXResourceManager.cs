using System;
using System.Threading;

namespace BouncyBox.VorpalEngine.Engine.DirectX
{
    /// <summary>Represents an object that manages entities.</summary>
    public interface IDirectXResourceManager<out TGameState> : IDisposable
        where TGameState : class
    {
        /// <summary>Releases render resources.</summary>
        void ReleaseRenderResources(CancellationToken cancellationToken);

        /// <summary>Renders a render state.</summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>Returns a tuple containing the result of the render attempt and a frametime if a frame was rendered.</returns>
        (RenderResult result, TimeSpan frametime) Render(CancellationToken cancellationToken);

        /// <summary>Handles dispatched render resources messages.</summary>
        void HandleDispatchedRenderResourcesMessages();
    }
}