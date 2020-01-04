# Nested Contexts

Nested contexts (`BouncyBox.VorpalEngine.Common.NestedContext`) are used to support tracking a chain of method calls across classes and objects. Usually, nested contexts are used for logging purposes. The engine often generates log messages that look identical except for their context, so it's important to also output the context with the message.

Here is some example log output showing nested contexts:

```text
[15:22:35.5517791 DBG] [EntityManager] Creating ID2D1Bitmap1 target
[15:22:35.5542706 DBG] [EntityManager] Setting target
[15:22:35.5570066 DBG] [EntityManager] Making window association
[15:22:35.5586890 DBG] [EntityManager] Creating DWriteFactory
[15:22:35.6004301 DBG] [UpdateWorker->ConcurrentMessageDispatchQueue] Dispatching RefreshPeriodChangedMessage
[15:22:35.6072691 DBG] [UpdateWorker->ConcurrentMessageDispatchQueue] Handling RefreshPeriodChangedMessage
[15:22:35.6090480 INF] [UnknownContext] Refresh period changed to ~6.9514 ms (~143.856 Hz)
```

In the above, `[UpdateWorker->ConcurrentMessageDispatchQueue]` indicates that the `UpdateWorker` class called the `ConcurrentMessageDispatchQueue` class, which then logged a message.

Nested contexts are implemented as a kind of stack, with each subsequent class or object optionally pushing a new context onto the stack. Some classes may choose to simply forward the nested context to other classes without pushing their own context.
