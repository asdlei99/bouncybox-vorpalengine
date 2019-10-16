using System;
using System.Threading;
using BouncyBox.VorpalEngine.Engine.Threads;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>
    ///     Runs the DirectX loop.
    /// </summary>
    internal sealed class DirectXLoop
    {
        private readonly IInterfaces _interfaces;

        public DirectXLoop(IInterfaces interfaces)
        {
            _interfaces = interfaces;
        }

        public void Run(CancellationToken cancellationToken = default)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.DirectX);

            TimeSpan sleepDuration = TimeSpan.FromMilliseconds(10);

            while (!cancellationToken.IsCancellationRequested)
            {
                cancellationToken.WaitHandle.WaitOne(sleepDuration);
            }
        }
    }
}