using BouncyBox.Common.NetStandard21.Logging;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Input.Keyboard;
using BouncyBox.VorpalEngine.Engine.Input.XInput;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Threads;

namespace BouncyBox.VorpalEngine.Engine
{
    /// <summary>
    ///     <para>Represents interfaces that are commonly used throughout the engine.</para>
    ///     <para>This interface is intended to ease constructor parameter and field count.</para>
    /// </summary>
    public interface IInterfaces
    {
        /// <summary>Gets the <see cref="ISerilogLogger" /> implementation.</summary>
        ISerilogLogger SerilogLogger { get; }

        /// <summary>Gets the <see cref="IThreadManager" /> implementation.</summary>
        IThreadManager ThreadManager { get; }

        /// <summary>Gets the <see cref="ICommonGameSettings" /> implementation.</summary>
        ICommonGameSettings CommonGameSettings { get; }

        /// <summary>Gets the global <see cref="IConcurrentMessageQueue{TMessageBase}" /> implementation.</summary>
        IConcurrentMessageQueue<IGlobalMessage> GlobalConcurrentMessageQueue { get; }

        /// <summary>Gets the update <see cref="IMessageQueue{TMessageBase}" /> implementation.</summary>
        IMessageQueue<IUpdateMessage> UpdateMessageQueue { get; }

        /// <summary>Gets the <see cref="IKeyboard" /> implementation.</summary>
        IKeyboard Keyboard { get; }

        /// <summary>Gets the <see cref="IStatefulGamepad" /> implementation.</summary>
        IStatefulGamepad StatefulGamepad { get; }
    }
}