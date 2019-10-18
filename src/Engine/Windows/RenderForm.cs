using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BouncyBox.VorpalEngine.Engine.Bootstrap;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Input.Keyboard;
using BouncyBox.VorpalEngine.Engine.Interop;
using BouncyBox.VorpalEngine.Engine.Logging;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using BouncyBox.VorpalEngine.Engine.Threads;
using TerraFX.Interop;
using User32 = TerraFX.Interop.User32;

namespace BouncyBox.VorpalEngine.Engine.Windows
{
    /// <summary>
    ///     A window used as a rendering target.
    /// </summary>
    internal sealed class RenderForm : Form
    {
        private readonly ConcurrentMessagePublisherSubscriber<IGlobalMessage> _globalMessagePublisherSubscriber;
        private readonly ContextFlag _ignoreWmSize = new ContextFlag();
        private readonly IInterfaces _interfaces;
        private readonly ProgramOptions _programOptions;
        private readonly ContextSerilogLogger _serilogLogger;
        private bool _isActivated;
        private bool _isMinimized;
        private bool _isUserMovingOrResizing;
        private IntPtr _monitorHandle = IntPtr.Zero;
        private Size _requestedResolution;
        private Message? _sizeMessage;
        private bool _wasMaximized;
        private WindowedMode _windowedMode;

        /// <summary>
        ///     <para>Initializes a new instance of the <see cref="RenderForm" /> type.</para>
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This constructor handles all configuration of the underlying window in preparation for rendering, including setting
        ///         window styles, the icon, the caption, the start position, and window control availability. It also registers Raw Input
        ///         devices, which send their messages to the render window.
        ///     </para>
        ///     <para>Subscribes to the <see cref="ResolutionRequestedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="WindowedModeRequestedMessage" /> global message.</para>
        ///     <para>Publishes the <see cref="RenderWindowHandleCreatedMessage" /> global message.</para>
        /// </remarks>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="programOptions">Parsed command line arguments.</param>
        /// <param name="context">A nested context.</param>
        /// <exception>Thrown when <see cref="TerraFX.Interop.User32.RegisterRawInputDevices" /> failed.</exception>
        public unsafe RenderForm(IInterfaces interfaces, ProgramOptions programOptions, NestedContext context)
        {
            context = context.CopyAndPush(nameof(RenderForm));

            _serilogLogger = new ContextSerilogLogger(interfaces.SerilogLogger, context);
            _interfaces = interfaces;
            _programOptions = programOptions;
            _globalMessagePublisherSubscriber =
                ConcurrentMessagePublisherSubscriber<IGlobalMessage>
                    .Create(interfaces, context)
                    .Subscribe<ResolutionRequestedMessage>(HandleResolutionRequestedMessage)
                    .Subscribe<WindowedModeRequestedMessage>(HandleWindowedModeRequestedMessage);

            using (_ignoreWmSize.Set())
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.Opaque, true);

                Icon = interfaces.CommonGameSettings.Icon;
                StartPosition = FormStartPosition.Manual;
                Text = interfaces.CommonGameSettings.Title;
                MaximizeBox = interfaces.CommonGameSettings.AllowUserResizing;
                SetFormBorderStyle();

                SetWindowedMode(interfaces.CommonGameSettings.WindowedMode);
                SetResolution(interfaces.CommonGameSettings.RequestedResolution, false);
            }

            _globalMessagePublisherSubscriber.Publish(new RenderWindowHandleCreatedMessage(Handle));

            SetMonitorHandle();

            // Register keyboard Raw Input device

            _serilogLogger.LogDebug("Registering keyboard Raw Input device");

            var rawInputDevice =
                new RAWINPUTDEVICE
                {
                    dwFlags = User32.RIDEV_INPUTSINK,
                    hwndTarget = Handle,
                    usUsage = TerraFX.Interop.Windows.HID_USAGE_GENERIC_KEYBOARD,
                    usUsagePage = TerraFX.Interop.Windows.HID_USAGE_PAGE_GENERIC
                };

            if (User32.RegisterRawInputDevices(&rawInputDevice, 1, (uint)sizeof(RAWINPUTDEVICE)) == TerraFX.Interop.Windows.FALSE)
            {
                throw Win32ExceptionHelper.GetException();
            }
        }

        /// <summary>
        ///     <para>Initializes a new instance of the <see cref="RenderForm" /> type.</para>
        ///     <para>
        ///         This constructor handles all configuration of the underlying window in preparation for rendering, including setting
        ///         window styles, the icon, the caption, the start position, and window control availability. It also registers Raw Input
        ///         devices, which send their messages to the render window.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     <para>Subscribes to the <see cref="ResolutionRequestedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="WindowedModeRequestedMessage" /> global message.</para>
        /// </remarks>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="programOptions">Parsed command line arguments.</param>
        /// <exception cref="Win32Exception">Thrown when <see cref="TerraFX.Interop.User32.RegisterRawInputDevices" /> failed.</exception>
        public RenderForm(IInterfaces interfaces, ProgramOptions programOptions)
            : this(interfaces, programOptions, NestedContext.None())
        {
        }

        /// <summary>
        ///     Instructs the global message publisher/subscriber to handle messages to which this object subscribed.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the main thread.</exception>
        public void HandleDispatchedMessages()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Main);

            _globalMessagePublisherSubscriber.HandleDispatched();
        }

        /// <inheritdoc />
        protected override bool ProcessDialogKey(Keys keyData)
        {
            // Don't allow menu actions
            return keyData == (Keys.Menu | Keys.Alt) || keyData == Keys.F10 || base.ProcessDialogKey(keyData);
        }

        /// <inheritdoc />
        protected override void WndProc(ref Message m)
        {
            if (_programOptions.LogWindowsMessages)
            {
                WindowMessage? windowsMessage = WindowMessage.MapKnown(m.Msg);
                var lParam = unchecked((ulong)m.LParam.ToInt64());
                var wParam = unchecked((ulong)m.WParam.ToInt64());

                _serilogLogger.LogDebug(
                    $"{(windowsMessage?.Name ?? $"0x{m.Msg.ToString("X").PadRight(8)}").PadRight(25)}    lParam: 0x{lParam.ToString("X").PadRight(8)}    wParam: 0x{wParam.ToString("X").PadRight(8)}");
            }

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch ((uint)m.Msg)
            {
                case User32.WM_ACTIVATEAPP:
                    HandleWindowActivation(ref m);
                    break;
                case User32.WM_CLOSE:
                    if (HandleClose(ref m) == HandleResult.Return)
                    {
                        return;
                    }
                    break;
                case User32.WM_DESTROY:
                    HandleWindowDestruction();
                    return;
                case User32.WM_DISPLAYCHANGE:
                    HandleDisplayResolutionChange();
                    break;
                case User32.WM_ENTERSIZEMOVE:
                    HandleEnterSizeMove();
                    break;
                case User32.WM_EXITSIZEMOVE:
                    HandleExitSizeMove();
                    break;
                case User32.WM_INPUT:
                    HandleInput(ref m);
                    break;
                case User32.WM_MENUCHAR:
                    HandleMenuChar(ref m);
                    break;
                case User32.WM_MOVE:
                    HandleMove();
                    break;
                case User32.WM_POWERBROADCAST:
                    HandlePowerBroadcast(ref m);
                    break;
                case User32.WM_SIZE:
                    HandleSize(ref m);
                    break;
                case User32.WM_SYSCOMMAND:
                    if (HandleSysCommand(ref m) == HandleResult.Return)
                    {
                        return;
                    }
                    break;
            }

            base.WndProc(ref m);
        }

        /// <summary>
        ///     Handles the <see cref="User32.WM_ACTIVATE" /> message.
        /// </summary>
        /// <remarks>
        ///     <para>Publishes the <see cref="RenderWindowActivatedMessage" /> global message.</para>
        ///     <para>Publishes the <see cref="RenderWindowDeactivatedMessage" /> global message.</para>
        /// </remarks>
        /// <param name="message">The associated Windows message.</param>
        private void HandleWindowActivation(ref Message message)
        {
            if (message.WParam.ToInt32() != 0)
            {
                _isActivated = true;
                _globalMessagePublisherSubscriber.Publish<RenderWindowActivatedMessage>();
            }
            else
            {
                _isActivated = false;
                _globalMessagePublisherSubscriber.Publish<RenderWindowDeactivatedMessage>();
            }

            // Reset keyboard state to prevent "key up" messages with no earlier "key down" message
            _interfaces.Keyboard.Reset();
        }

        /// <summary>
        ///     Handles the <see cref="User32.WM_CLOSE" /> message.
        /// </summary>
        /// <remarks>
        ///     Publishes the <see cref="RenderWindowClosingMessage" /> global message.
        /// </remarks>
        private HandleResult HandleClose(ref Message message)
        {
            message.Result = IntPtr.Zero;

            // Hide the window while waiting for engine threads to terminate
            Visible = false;

            _globalMessagePublisherSubscriber.Publish<RenderWindowClosingMessage>();

            return HandleResult.Return;
        }

        /// <summary>
        ///     Handles the <see cref="User32.WM_DESTROY" /> message.
        /// </summary>
        private void HandleWindowDestruction()
        {
            _globalMessagePublisherSubscriber.Dispose();
        }

        /// <summary>
        ///     Handles the <see cref="User32.WM_DISPLAYCHANGE" /> message.
        /// </summary>
        private void HandleDisplayResolutionChange()
        {
            // TODO
        }

        /// <summary>
        ///     Handles the <see cref="User32.WM_ENTERSIZEMOVE" /> message.
        /// </summary>
        private void HandleEnterSizeMove()
        {
            _isUserMovingOrResizing = true;
        }

        /// <summary>
        ///     Handles the <see cref="User32.WM_EXITSIZEMOVE" /> message.
        /// </summary>
        private void HandleExitSizeMove()
        {
            _isUserMovingOrResizing = false;

            if (_sizeMessage != null)
            {
                Message message = _sizeMessage.Value;

                HandleSize(ref message, true);
            }

            _sizeMessage = null;
        }

        /// <summary>
        ///     Handles the <see cref="User32.WM_INPUT" /> message.
        /// </summary>
        /// <param name="message">The associated Windows message.</param>
        /// <exception>Thrown when <see cref="User32.GetRawInputData" /> failed.</exception>
        private unsafe void HandleInput(ref Message message)
        {
            if (!_isActivated)
            {
                return;
            }

            uint cbSize;
            uint result = User32.GetRawInputData(message.LParam, User32.RID_INPUT, null, &cbSize, (uint)sizeof(RAWINPUTHEADER));

            if (result == unchecked((uint)-1))
            {
                throw Win32ExceptionHelper.GetException();
            }

            RAWINPUT rawInput;

            result = User32.GetRawInputData(message.LParam, User32.RID_INPUT, &rawInput, &cbSize, (uint)sizeof(RAWINPUTHEADER));

            if (result == unchecked((uint)-1))
            {
                throw Win32ExceptionHelper.GetException();
            }

            var vKey = (Interop.User32.VirtualKey)rawInput.data.keyboard.VKey;
            Interop.User32.VirtualKey mappedKey = vKey switch
            {
                Interop.User32.VirtualKey.VK_SHIFT => (rawInput.data.keyboard.MakeCode switch
                                                          {
                                                              0x2A => Interop.User32.VirtualKey.VK_LSHIFT,
                                                              0x36 => Interop.User32.VirtualKey.VK_RSHIFT,
                                                              _ => Interop.User32.VirtualKey.VK_SHIFT
                                                          }),
                Interop.User32.VirtualKey.VK_CONTROL => (rawInput.data.keyboard.Flags & User32.RI_KEY_E0) == User32.RI_KEY_E0
                                                            ? Interop.User32.VirtualKey.VK_RCONTROL
                                                            : Interop.User32.VirtualKey.VK_LCONTROL,
                Interop.User32.VirtualKey.VK_MENU => (rawInput.data.keyboard.Flags & User32.RI_KEY_E0) == User32.RI_KEY_E0
                                                         ? Interop.User32.VirtualKey.VK_RMENU
                                                         : Interop.User32.VirtualKey.VK_LMENU,
                _ => vKey
            };

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (rawInput.data.keyboard.Message)
            {
                case User32.WM_KEYDOWN:
                case User32.WM_SYSKEYDOWN:
                    _interfaces.Keyboard.EnqueueStateChange(KeyState.Down, mappedKey);
                    break;
                case User32.WM_KEYUP:
                case User32.WM_SYSKEYUP:
                    _interfaces.Keyboard.EnqueueStateChange(KeyState.Up, mappedKey);
                    break;
            }
        }

        /// <summary>
        ///     Handles the <see cref="User32.WM_MENUCHAR" /> message.
        /// </summary>
        /// <param name="message">The associated Windows message.</param>
        private static void HandleMenuChar(ref Message message)
        {
            message.Result = new IntPtr(User32.MNC_CLOSE << 16);
        }

        /// <summary>
        ///     Handles the <see cref="User32.WM_MOVE" /> message.
        /// </summary>
        private void HandleMove()
        {
            SetMonitorHandle();
        }

        /// <summary>
        ///     Handles the <see cref="User32.WM_POWERBROADCAST" /> message.
        /// </summary>
        /// <param name="message">The associated Windows message.</param>
        private static void HandlePowerBroadcast(ref Message message)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch ((uint)message.WParam.ToInt32())
            {
                case User32.PBT_APMQUERYSUSPEND:
                    message.Result = new IntPtr(1);
                    break;
                case User32.PBT_APMRESUMESUSPEND:
                    break;
            }
        }

        /// <summary>
        ///     Handles the <see cref="User32.WM_SIZE" /> message.
        /// </summary>
        /// <remarks>
        ///     <para>Publishes the <see cref="RenderWindowMinimizedMessage" /> global message.</para>
        ///     <para>Publishes the <see cref="RenderWindowRestoredMessage" /> global message.</para>
        /// </remarks>
        /// <param name="message">The associated Windows message.</param>
        /// <param name="userResizeComplete">A value indicating if a user resize in progress was completed.</param>
        private void HandleSize(ref Message message, bool userResizeComplete = false)
        {
            SetMonitorHandle();

            if (_ignoreWmSize)
            {
                return;
            }
            if (_isUserMovingOrResizing)
            {
                // Track the last WM_SIZE message that was received so it can be processed after WM_EXITSIZEMOVE
                _sizeMessage = message;
                return;
            }

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (message.WParam.ToInt32())
            {
                case User32.SIZE_MAXIMIZED:
                    // If the user maximizes the window then treat it as a requested resolution change
                    SetResolution(ClientSize, true);
                    break;
                case User32.SIZE_MINIMIZED:
                    if (!_isMinimized)
                    {
                        _isMinimized = true;
                        _globalMessagePublisherSubscriber.Publish(new RenderWindowMinimizedMessage());
                    }
                    break;
                case User32.SIZE_RESTORED:
                    if (_isMinimized)
                    {
                        _isMinimized = false;
                        _globalMessagePublisherSubscriber.Publish(new RenderWindowRestoredMessage());
                    }

                    // If the user resizes the window then treat it as a requested resolution change
                    SetResolution(ClientSize, userResizeComplete);
                    break;
            }
        }

        /// <summary>
        ///     Handles the <see cref="User32.WM_SYSCOMMAND" /> message.
        /// </summary>
        /// <param name="message">The associated Windows message.</param>
        private HandleResult HandleSysCommand(ref Message message)
        {
            if (message.WParam.ToInt32() == User32.SC_KEYMENU && (int)message.LParam >> 16 <= 0)
            {
                return HandleResult.Return;
            }

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (message.WParam.ToInt32() & 0xFFF0)
            {
                case User32.SC_MONITORPOWER:
                    // ReSharper disable once SwitchStatementMissingSomeCases
                    switch (message.LParam.ToInt32())
                    {
                        case -1:
                            // Display power-on
                            break;
                        case 1:
                            // Display low-power
                            break;
                        case 2:
                            // Display off
                            break;
                    }
                    break;
                case User32.SC_SCREENSAVE:
                    if (!_interfaces.CommonGameSettings.AllowScreensaver)
                    {
                        message.Result = IntPtr.Zero;
                    }
                    break;
            }

            return HandleResult.InvokeBaseWndProc;
        }

        /// <summary>
        ///     Detects the display the render form is considered to be on according to Windows and tracks the resulting monitor handle.
        /// </summary>
        /// <remarks>
        ///     Publishes the <see cref="DisplayChangedMessage" /> global message.
        /// </remarks>
        /// <exception>Thrown when <see cref="User32.GetMonitorInfoW" /> failed.</exception>
        /// <exception>Thrown when <see cref="User32.EnumDisplayDevicesW" /> failed.</exception>
        private unsafe void SetMonitorHandle()
        {
            IntPtr monitorHandle = User32.MonitorFromWindow(Handle, User32.MONITOR_DEFAULTTONEAREST);

            if (monitorHandle == _monitorHandle)
            {
                return;
            }

            _monitorHandle = monitorHandle;

            // Get display information

            MONITORINFOEXW monitorInfoExW;

            monitorInfoExW._base.cbSize = (uint)sizeof(MONITORINFOEXW);

            if (User32.GetMonitorInfoW(monitorHandle, &monitorInfoExW._base) == TerraFX.Interop.Windows.FALSE)
            {
                throw Win32ExceptionHelper.GetException();
            }

            // Get display device information for logging purposes

            DISPLAY_DEVICEW displayDeviceW;

            displayDeviceW.cb = (uint)sizeof(DISPLAY_DEVICEW);

            if (User32.EnumDisplayDevicesW(monitorInfoExW.szDevice, 0, &displayDeviceW, 0) == TerraFX.Interop.Windows.FALSE)
            {
                throw Win32ExceptionHelper.GetException();
            }

            var deviceString = new string((char*)displayDeviceW.DeviceString);

            _serilogLogger.LogInformation("Display changed to {DeviceName} (0x{MonitorHandle})", deviceString, monitorHandle.ToString("X8"));

            _globalMessagePublisherSubscriber.Publish(new DisplayChangedMessage(monitorHandle));
        }

        /// <summary>
        ///     <para>Requests a change in game resolution, interpreted as a change to the render window's client size.</para>
        ///     <para>
        ///         The actual resolution after processing the request may be different from the requested resolution due to render window
        ///         configuration.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     Publishes the <see cref="ResolutionChangedMessage" /> global message.
        /// </remarks>
        /// <param name="requestedResolution">The requested resolution.</param>
        /// <param name="userRequested">A value indicating whether the user requested the change.</param>
        private void SetResolution(Size requestedResolution, bool userRequested)
        {
            if (requestedResolution == _requestedResolution)
            {
                return;
            }

            using (_ignoreWmSize.Set())
            {
                // If something other than the user is setting the resolution, restore the window
                if (!userRequested && WindowState == FormWindowState.Maximized)
                {
                    WindowState = FormWindowState.Normal;
                }

                _requestedResolution = requestedResolution;
                ClientSize = requestedResolution;
                // Only center the rendering form if something other than the user resized it
                if (!userRequested)
                {
                    CenterToScreen();
                }
            }

            _serilogLogger.LogInformation(
                "Requested resolution was {RequestedWidth}x{RequestedHeight}; actual resolution is now {ActualWidth}x{ActualHeight}",
                requestedResolution.Width,
                requestedResolution.Height,
                ClientSize.Width,
                ClientSize.Height);

            _globalMessagePublisherSubscriber.Publish(new ResolutionChangedMessage(ClientSize));
        }

        /// <summary>
        ///     <para>Sets the windowed mode of the render window.</para>
        ///     <para>This method intelligently handles maximized windows when calculating new render window position and size.</para>
        /// </summary>
        /// <param name="mode">A windowed mode to set.</param>
        private void SetWindowedMode(WindowedMode mode)
        {
            if (_windowedMode == mode)
            {
                return;
            }

            using (_ignoreWmSize.Set())
            {
                _windowedMode = mode;
                ControlBox = mode == WindowedMode.BorderedWindowed;
                SetFormBorderStyle();

                if (WindowState == FormWindowState.Maximized)
                {
                    _wasMaximized = true;

                    // Treat the maximized state as a request for windowed fullscreen

                    Screen screen = Screen.FromHandle(Handle);

                    WindowState = FormWindowState.Normal;
                    Bounds = screen.Bounds;
                    // Changing border styles can cause the potential client size to change, so set the requested resolution again
                    SetResolution(ClientSize, false);
                }
                else if (_wasMaximized)
                {
                    // Transitioning from windowed fullscreen back to maximized

                    _wasMaximized = false;
                    WindowState = FormWindowState.Maximized;
                }
                else
                {
                    // Changing border styles can cause the potential client size to change, so set the requested resolution again
                    SetResolution(_requestedResolution, false);
                    CenterToScreen();
                }
            }

            _serilogLogger.LogInformation(mode == WindowedMode.BorderedWindowed ? "Window is now bordered" : "Window is now borderless");
        }

        /// <summary>
        ///     Sets the window's border style.
        /// </summary>
        private void SetFormBorderStyle()
        {
            FormBorderStyle =
                _windowedMode == WindowedMode.BorderedWindowed
                    ? _interfaces.CommonGameSettings.AllowUserResizing ? FormBorderStyle.Sizable : FormBorderStyle.FixedSingle
                    : FormBorderStyle.None;
        }

        /// <summary>
        ///     Handles the <see cref="ResolutionRequestedMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleResolutionRequestedMessage(ResolutionRequestedMessage message)
        {
            SetResolution(message.Resolution, false);
        }

        /// <summary>
        ///     Handles the <see cref="WindowedModeRequestedMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleWindowedModeRequestedMessage(WindowedModeRequestedMessage message)
        {
            SetWindowedMode(message.Mode);
        }

        /// <summary>
        ///     Determines whether to call the window's window procedure after processing a Windows message.
        /// </summary>
        private enum HandleResult
        {
            /// <summary>
            ///     Do not call the window's window procedure.
            /// </summary>
            Return,

            /// <summary>
            ///     Call the window's window procedure.
            /// </summary>
            InvokeBaseWndProc
        }
    }
}