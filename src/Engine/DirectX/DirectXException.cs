﻿using System;
using System.Runtime.Serialization;

namespace BouncyBox.VorpalEngine.Engine.DirectX
{
    /// <inheritdoc />
    /// <summary>
    ///     Exception thrown when an error occurs interacting with DirectX COM objects.
    /// </summary>
    [Serializable]
    internal class DirectXException : Exception
    {
        /// <inheritdoc />
        public DirectXException()
        {
        }

        /// <inheritdoc />
        protected DirectXException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        public DirectXException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public DirectXException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}