using System.Collections.Generic;
using System.Diagnostics;
using EnumsNET;

namespace BouncyBox.VorpalEngine.Engine.Input.XInput
{
    /// <summary>
    ///     A stateful XInput gamepad.
    /// </summary>
    public class StatefulGamepad : IStatefulGamepad
    {
        private readonly HashSet<XInputVirtualKey> _downKeys = new HashSet<XInputVirtualKey>();
        private readonly Gamepad _gamepad;
        private readonly HashSet<XInputVirtualKey> _repeatKeys = new HashSet<XInputVirtualKey>();
        private readonly HashSet<XInputVirtualKey> _upKeys = new HashSet<XInputVirtualKey>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="StatefulGamepad" /> type.
        /// </summary>
        /// <param name="user">A user.</param>
        public StatefulGamepad(XInputUser user = XInputUser.Any)
        {
            _gamepad = new Gamepad((byte)user);
        }

        /// <inheritdoc />
        public IReadOnlyCollection<XInputVirtualKey> DownKeys => _downKeys;

        /// <inheritdoc />
        public IReadOnlyCollection<XInputVirtualKey> UpKeys => _upKeys;

        /// <inheritdoc />
        public IReadOnlyCollection<XInputVirtualKey> RepeatKeys => _repeatKeys;

        /// <inheritdoc />
        public UpdateResult Update()
        {
            (GetKeystrokeResult result, XInputKeystroke? keystroke) = _gamepad.GetKeystroke();

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (result)
            {
                case GetKeystrokeResult.DeviceNotConnected:
                    return UpdateResult.DeviceNotConnected;
                case GetKeystrokeResult.Empty:
                    return UpdateResult.Success;
            }

            Debug.Assert(keystroke != null);

            // No complex state management for repeating keys, since they are moments in time
            _repeatKeys.Clear();

            XInputKeystrokeFlags keystrokeFlags = keystroke.Value.Flags;
            XInputVirtualKey virtualKey = keystroke.Value.VirtualKey;

            if (keystrokeFlags.HasAllFlags(XInputKeystrokeFlags.Repeat))
            {
                _repeatKeys.Add(virtualKey);

                return UpdateResult.Success;
            }

            bool isKeyDown = keystrokeFlags.HasAllFlags(XInputKeystrokeFlags.KeyDown);
            bool isKeyUp = keystrokeFlags.HasAllFlags(XInputKeystrokeFlags.KeyUp);

            if (isKeyDown && isKeyUp)
            {
                // The key was pressed and released since the previous update
                _downKeys.Add(virtualKey);
                _upKeys.Add(virtualKey);
            }
            else if (isKeyDown)
            {
                // The key was pressed since the previous update
                _downKeys.Add(virtualKey);
                _upKeys.Remove(virtualKey);
            }
            else if (isKeyUp)
            {
                // The key was released since the previous update
                _downKeys.Remove(virtualKey);
                _upKeys.Add(virtualKey);
            }

            return UpdateResult.Success;
        }
    }
}