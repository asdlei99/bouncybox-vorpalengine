﻿using System;
using System.Diagnostics;
using BouncyBox.VorpalEngine.Engine.Threads;
using ProcessThread = BouncyBox.VorpalEngine.Engine.Threads.ProcessThread;

namespace BouncyBox.VorpalEngine.Engine
{
    /// <summary>Provides helper methods for implementing the dispose pattern.</summary>
    [DebuggerStepThrough]
    public static class DisposeHelper
    {
        /// <summary>Disposes an object only if the provided flag is false. The flag is set to true after disposal.</summary>
        /// <param name="delegate">A delegate that disposes the object.</param>
        /// <param name="isDisposed">A flag indicating if the object was disposed.</param>
        public static void Dispose(Action @delegate, ref bool isDisposed)
        {
            if (isDisposed)
            {
                return;
            }

            @delegate();
            isDisposed = true;
        }

        /// <summary>Disposes an object only if the provided flag is false. The flag is set to true after disposal.</summary>
        /// <param name="delegate">A delegate that disposes the object.</param>
        /// <param name="isDisposed">A flag indicating if the object was disposed.</param>
        /// <param name="threadManager">An <see cref="IThreadManager" /> implementation.</param>
        /// <param name="thread">The thread on which the method must be executed.</param>
        public static void Dispose(Action @delegate, ref bool isDisposed, IThreadManager threadManager, ProcessThread thread)
        {
            threadManager.VerifyProcessThread(thread);

            if (isDisposed)
            {
                return;
            }

            @delegate();
            isDisposed = true;
        }
    }
}