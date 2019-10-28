using System;
using System.Diagnostics;

namespace BouncyBox.VorpalEngine.Common
{
    /// <summary>
    ///     Counts the number of times <see cref="Set" /> is called and sets a flag to true if the number of <see cref="Set" /> calls are
    ///     less than or equal to the number of <see cref="Dispose" /> calls.
    /// </summary>
    [DebuggerStepThrough]
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class ContextFlag : IDisposable
    {
        private int _counter;
        private bool _isDisposed;

        /// <summary>
        ///     Gets a value determining if the flag is set. The flag is considered set when the the number of <see cref="Set" /> calls are
        ///     less than or equal to the number of <see cref="Dispose" /> calls.
        /// </summary>
        public bool Flag => _counter > 0;

        private string DebuggerDisplay => "Flag = " + Flag;

        /// <summary>Decrements the counter by one.</summary>
        public void Dispose()
        {
            DisposeHelper.Dispose(() => { _counter = System.Math.Max(0, _counter - 1); }, ref _isDisposed);
        }

        /// <summary>
        ///     <para>Increments the counter by one.</para>
        ///     <para>It is intended to call this method from within a using block.</para>
        /// </summary>
        /// <returns>Returns this object.</returns>
        public IDisposable Set()
        {
            _counter++;

            return this;
        }

        /// <summary>Returns the flag.</summary>
        public static implicit operator bool(ContextFlag contextFlag)
        {
            return contextFlag.Flag;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return DebuggerDisplay;
        }
    }
}