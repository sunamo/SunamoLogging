# SunamoLogging

## Short description

Podpora několika logovacích systémů podle výstupu: file logger, debug/dummy/sunamo loggery (včetně typovaných a template variant), `LogRouter` s providerem, `LoggingBootstrap`, `ConsoleTee` a `CrashHandler`. Staví na `Microsoft.Extensions.Logging`.
Balíček je self-contained: kód dříve referencovaných balíčků (SunamoCl, SunamoDependencyInjection, SunamoPlatformUwpInterop aj.) je zkopírován do `_sunamo\` jako internal a jiné Sunamo balíčky nereferencuje (SunamoCl používá jen pomocný projekt `RunnerLogging`).

Support for several logging system based on their output

## Overview

SunamoLogging is part of the Sunamo package ecosystem, providing modular, platform-independent utilities for .NET development.

## Main Components

### Key Classes

- **as**
- **FileLogger**
- **FileLoggerExtensions**
- **FileLoggerProvider**
- **ILoggerExtensions**
- **InitApp**
- **DebugLogger**

### Key Methods

- `ClipboardOrDebug()`
- `WriteLineFormat()`
- `WriteCount()`
- `WriteList()`
- `WriteListOneRow()`
- `SavedToDrive()`
- `TryAFewSecondsLaterAfterFullyInitialized()`
- `Finished()`
- `EndRunTime()`
- `ResultCopiedToClipboard()`

## Installation

```bash
dotnet add package SunamoLogging
```

## Dependencies

- **Microsoft.Extensions.Logging.Abstractions** (v9.0.5)

## Package Information

- **Package Name**: SunamoLogging
- **Version**: 25.6.7.1
- **Target Framework**: net9.0
- **Category**: Platform-Independent NuGet Package
- **Source Files**: 34

## Related Packages

This package is part of the Sunamo package ecosystem. For more information about related packages, visit the main repository.

## License

See the repository root for license information.

<!-- sunamo:test-coverage:start -->
## Test coverage

| Metric | Value |
|---|---|
| Method | static |
| Test projects | 1 |
| Public types referenced by tests | 0/28 (0.0 %) |
| Public methods referenced by tests | 0/80 (0.0 %) |
| Line coverage | n/a |
| Branch coverage | n/a |
| Generated (UTC) | 2026-11-22 21:24 |

<!-- sunamo:test-coverage:data {"package":"SunamoLogging","container":"pinp","testProjects":1,"publicTypes":28,"typesReferencedByTests":0,"publicMethods":80,"methodsReferencedByTests":0,"lineRate":null,"branchRate":null,"method":"static","generatedUtc":"2026-11-22 21:24"} -->
<!-- sunamo:test-coverage:end -->
