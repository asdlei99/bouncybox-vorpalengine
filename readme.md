# Vorpal Engine

## Build status

Automated build support has not yet been added.

## What is Vorpal Engine?

I've dabbled with game engines many times over the years, but I was never happy with using C++ for them. I'm always far more productive using C# and .NET. Recently, drastic improvements in C# and .NET--specifically C# 8.0 and .NET Core 3.x--have allowed me to once again attempt a game engine in C# and .NET.

Vorpal Engine is that attempt. For now it will be a 2D engine due to my relative lack of interest in designing 3D games from scratch (use a commercial engine for 3D games!) I'll eventually use the engine to write a game of some kind.

This engine is really just a hobby project at this time. I don't have any grand plans for it and I don't plan on commercializing it.

## What is Bouncy Box?

Bouncy Box is my not-yet-registered LLC. You'll see this name references in various namespaces and other artifacts. For now it's synonymous with my person. This name is subject to change.

## Engine requirements and recommendations

- Windows 10
- A GPU that supports DirectX 11
- A gamepad that supports XInput or can be mapped with a mapping application like [DS4Windows](https://ryochan7.github.io/ds4windows-site/)

## Software development requirements and recommendations

- [Visual Studio 2019](https://visualstudio.microsoft.com/vs/)
- [ReSharper](https://www.jetbrains.com/resharper/)

## Platform interop

In the past, various DirectX interop projects like SlimDX and SharpDX existed, but those projects have since been abandoned. There are newer attempts at generating DirectX bindings, but those attempts use SharpGen, which I found difficult to use and difficult to understand. Upon discovering the DirectX discord (link below), I met Tanner Gooding, an employee for Microsoft who is working on a project he calls [TerraFX](https://github.com/terrafx). Several of the TerraFX libraries used by the engine contain bindings for numerous Windows header files and are auto-generated by [ClangSharp](https://github.com/Microsoft/ClangSharp). Combined with Tanner's efforts manually migrating innumerable C `#define`s, `terrafx.interop` libraries provides the perfect basis for a DirectX wrapper: low-level, precise, auto-generated mappings that rely on pointers instead of slower marshalling.

C# 9 may include a couple of key features that will make `terrafx.interop` bindings perform even better: compiler intrinsics and function pointers (see [this](https://github.com/dotnet/csharplang/issues/191) GitHub issue). SharpGen bindings already generate IL with these instructions so technically they may perform better than `terrafx.interop`, but I view this as only temporary. I believe it's better to build a wrapper on the rawest bindings possible--bindings that avoid marshalling where possible.

## Engine architecture

If I can find the time I will be sure to thoroughly document the engine's architecture. I am proud of my approach to this challenge and look forward to sharing my thoughts. I'll also try and do some diagramming to help communicate the architecture visually.

For now, I encourage you to start from one of the sample "game" projects and drill into the code that way.

## Third-party libraries

I rely on the following libraries:

- Dependency injection: [Autofac](https://autofac.org/)
- Command line parsing: [CommandLineParser](https://github.com/commandlineparser/commandline)
- Enumeration extensions: [Enums.NET](https://github.com/TylerBrinkley/Enums.NET)
- JSON: [Json.NET](https://www.newtonsoft.com/json)
- Logging: [Serilog](https://serilog.net/)
- Platform bindings: [TerraFX](https://github.com/terrafx/)

## Unit tests

I have not yet had the time to write unit tests. I won't write tests until I feel that the architecture of the engine is stable. Unit tests take a long time to write and act as a tight coupling to production code, so writing them when the core designs of the engine are still in flux would simply waste too much time.

## Sample "games"

### `DebuggingGame`

`DebuggingGame` is used mainly to exercise the `Game` class and to report engine statistics. This project has proven very useful to help me visualize what's going on inside the engine.

### `SampleGame`

`SampleGame` is intended to be a "real" game in the sense that it exercises more of the engine's capabilities than `DebuggingGame`.

## Code documentation

I am trying to be very thorough about documenting my code, both with XML documentations and comments.

## Code hygiene

I use ReSharper to keep the code consistently styled and formatted. The settings I use are included in a solution-wide settings file that lives next to the solution. I actively maintain my ReSharper subscription so I'm always using the latest GA release.

You may notice comments and attributes throughout the code that reference ReSharper; these comments and attributes hide suggestions and warnings that would otherwise interfere with my development experience.

## Contributions

I'm not accepting pull request contributions at this time as I simply lack the time to treat this as an "official" open source project. You are free to peruse the code and fork at your leisure. Feel free to contact me directly if you want to discuss bugs or possible improvements.

## Contacting me

Find me on Discord:

- [C#](https://discord.gg/csharp)
- [DirectX](https://discord.gg/N2mtwy)

My username is `NathanAldenSr#2130`.

## Acknowledgement and thanks

I couldn't have gotten this far this without the help of the very kind and super-intelligent and experienced folks on the two Discord servers I mentioned above. Special thanks goes out to Tanner Gooding for his amazing efforts on `terrafx.interop` and his willingness to answer my seemingly endless questions.
