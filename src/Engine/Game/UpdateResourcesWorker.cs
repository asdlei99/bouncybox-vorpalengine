using BouncyBox.VorpalEngine.Engine.Threads;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>An engine thread worker that manages requests to initialize, resize, and release non-DirectX resources.</summary>
    public class UpdateResourcesWorker : EngineThreadWorker
    {
        /// <summary>Initializes a new instance of the <see cref="UpdateResourcesWorker" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="context"></param>
        public UpdateResourcesWorker(IInterfaces interfaces, NestedContext context) : base(interfaces, EngineThread.UpdateResources, context)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateResourcesWorker" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        public UpdateResourcesWorker(IInterfaces interfaces) : this(interfaces, NestedContext.None())
        {
        }
    }
}