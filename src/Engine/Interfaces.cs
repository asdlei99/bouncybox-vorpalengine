using BouncyBox.Common.NetStandard21.Logging;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Input.Keyboard;
using BouncyBox.VorpalEngine.Engine.Input.XInput;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Threads;

namespace BouncyBox.VorpalEngine.Engine
{
    /// <summary>Interfaces that are commonly used throughout the engine.</summary>
    public class Interfaces : IInterfaces
    {
        /// <summary>
        ///     <para>Initializes a new instance of the <see cref="Interfaces" /> type.</para>
        ///     <para>All parameters are intended to be dependency-injected.</para>
        /// </summary>
        /// <param name="serilogLogger">An <see cref="ISerilogLogger" /> implementation.</param>
        /// <param name="threadManager">An <see cref="IThreadManager" /> implementation.</param>
        /// <param name="commonGameSettings">An <see cref="ICommonGameSettings" /> implementation.</param>
        /// <param name="globalConcurrentMessageQueue">An <see cref="IConcurrentMessageQueue{TMessageBase}" /> implementation.</param>
        /// <param name="updateMessageQueue">An update <see cref="IMessageQueue{TMessageBase}" /> implementation.</param>
        /// <param name="keyboard">An <see cref="IKeyboard" /> implementation.</param>
        /// <param name="statefulGamepad">An <see cref="IStatefulGamepad" /> implementation.</param>
        public Interfaces(
            ISerilogLogger serilogLogger,
            IThreadManager threadManager,
            ICommonGameSettings commonGameSettings,
            IConcurrentMessageQueue<IGlobalMessage> globalConcurrentMessageQueue,
            IMessageQueue<IUpdateMessage> updateMessageQueue,
            IKeyboard keyboard,
            IStatefulGamepad statefulGamepad)
        {
            SerilogLogger = serilogLogger;
            ThreadManager = threadManager;
            CommonGameSettings = commonGameSettings;
            GlobalConcurrentMessageQueue = globalConcurrentMessageQueue;
            UpdateMessageQueue = updateMessageQueue;
            Keyboard = keyboard;
            StatefulGamepad = statefulGamepad;
        }

        /// <inheritdoc />
        public ISerilogLogger SerilogLogger { get; }

        /// <inheritdoc />
        public IThreadManager ThreadManager { get; }

        /// <inheritdoc />
        public ICommonGameSettings CommonGameSettings { get; }

        /// <inheritdoc />
        public IConcurrentMessageQueue<IGlobalMessage> GlobalConcurrentMessageQueue { get; }

        /// <inheritdoc />
        public IMessageQueue<IUpdateMessage> UpdateMessageQueue { get; }

        /// <inheritdoc />
        public IKeyboard Keyboard { get; }

        /// <inheritdoc />
        public IStatefulGamepad StatefulGamepad { get; }
    }
}