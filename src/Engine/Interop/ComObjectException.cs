using System;
using System.Runtime.Serialization;

namespace BouncyBox.VorpalEngine.Engine.Interop
{
    /// <inheritdoc />
    /// <summary>Exception thrown by COM objects.</summary>
    [Serializable]
    internal class ComObjectException : Exception
    {
        /// <inheritdoc />
        public ComObjectException()
        {
        }

        /// <inheritdoc />
        protected ComObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        public ComObjectException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public ComObjectException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}