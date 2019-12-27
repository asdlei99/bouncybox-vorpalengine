using System;

namespace BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages
{
    /// <summary>A global message requesting that an object be disposed.</summary>
    public readonly struct DisposeObjectMessage : IGlobalMessage
    {
        /// <summary>Gets the object to be disposed.</summary>
        public IDisposable Disposable { get; }

        /// <summary>Initializes a new instance of the <see cref="DisposeObjectMessage" /> type.</summary>
        /// <param name="disposable">The object to be disposed.</param>
        public DisposeObjectMessage(IDisposable disposable)
        {
            Disposable = disposable;
        }
    }
}