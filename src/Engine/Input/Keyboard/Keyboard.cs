using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using BouncyBox.VorpalEngine.Interop;
using EnumsNET;

namespace BouncyBox.VorpalEngine.Engine.Input.Keyboard
{
    /// <summary>The keyboard.</summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class Keyboard : IKeyboard
    {
        private readonly HashSet<User32.VirtualKey> _downKeys = new HashSet<User32.VirtualKey>();
        private readonly object _lockObject = new object();

        private readonly ConcurrentQueue<(KeyState state, User32.VirtualKey key)> _stateChangeQueue =
            new ConcurrentQueue<(KeyState state, User32.VirtualKey key)>();

        private string DebuggerDisplay
        {
            get
            {
                lock (_lockObject)
                {
                    return $"DownKeys = {string.Join(", ", _downKeys.Select(a => a.GetName()))}";
                }
            }
        }

        /// <inheritdoc />
        public void EnqueueStateChange(KeyState state, User32.VirtualKey key)
        {
            _stateChangeQueue.Enqueue((state, key));
        }

        /// <inheritdoc />
        public void Reset()
        {
            lock (_lockObject)
            {
                _stateChangeQueue.Clear();
                _downKeys.Clear();
            }
        }

        /// <inheritdoc />
        public KeyboardSnapshot ProcessQueueAndSnapshot()
        {
            lock (_lockObject)
            {
                while (_stateChangeQueue.TryDequeue(out (KeyState state, User32.VirtualKey key) tuple))
                {
                    switch (tuple.state)
                    {
                        case KeyState.Down:
                            _downKeys.Add(tuple.key);
                            break;
                        case KeyState.Up:
                            _downKeys.Remove(tuple.key);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(tuple.state), tuple.state, null);
                    }
                }

                return new KeyboardSnapshot(_downKeys.ToImmutableArray());
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return DebuggerDisplay;
        }
    }
}