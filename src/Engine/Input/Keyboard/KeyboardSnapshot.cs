using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using BouncyBox.VorpalEngine.Engine.Interop;
using EnumsNET;

namespace BouncyBox.VorpalEngine.Engine.Input.Keyboard
{
    /// <summary>A snapshot of the keyboard.</summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public readonly struct KeyboardSnapshot
    {
        /// <summary>Initializes a new instance of the <see cref="KeyboardSnapshot" /> type.</summary>
        /// <param name="downKeys">The keys that were down at the time of the snapshot.</param>
        public KeyboardSnapshot(IReadOnlyCollection<User32.VirtualKey> downKeys)
        {
            DownKeys = downKeys;
        }

        private string DebuggerDisplay => $"DownKeys = {string.Join(", ", DownKeys.Select(a => a.GetName()))}";

        /// <summary>Gets the keys that were down at the time of the snapshot.</summary>
        public IReadOnlyCollection<User32.VirtualKey> DownKeys { get; }

        /// <summary>Determines if a key was down at the time of the snapshot.</summary>
        /// <param name="key">A key.</param>
        /// <returns>Returns true if the key was down at the time of the snapshot; otherwise, returns false.</returns>
        public bool IsKeyDown(User32.VirtualKey key)
        {
            return DownKeys.Contains(key);
        }

        /// <summary>Determines if a key was up at the time of the snapshot.</summary>
        /// <param name="key">A key.</param>
        /// <returns>Returns true if the key was up at the time of the snapshot; otherwise, returns false.</returns>
        public bool IsKeyUp(User32.VirtualKey key)
        {
            return !DownKeys.Contains(key);
        }

        /// <summary>Compares this snapshot with a previous snapshot to determine which keys are newly-down and newly-up.</summary>
        /// <param name="previousSnapshot">A previous snapshot.</param>
        /// <returns>Returns a tuple containing the newly-down and newly-up keys.</returns>
        public (IReadOnlyCollection<User32.VirtualKey> downKeys, IReadOnlyCollection<User32.VirtualKey> upKeys) GetChangedKeys(
            ref KeyboardSnapshot previousSnapshot)
        {
            return (DownKeys.Except(previousSnapshot.DownKeys).ToImmutableArray(), previousSnapshot.DownKeys.Except(DownKeys).ToImmutableArray());
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return DebuggerDisplay;
        }
    }
}