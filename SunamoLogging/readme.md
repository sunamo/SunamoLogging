# SunamoLogging

A modular, platform-independent logging library for .NET providing multiple logger implementations with different output targets.

## Features

- **LoggerBase** - Base class for simple text-based logging with format string support
- **TemplateLoggerBase** - Template logger with predefined message types (success, error, warning, info)
- **TypedLoggerBase** - Typed logger using enum-based message categories
- **FileLogger** - ILogger implementation writing to daily log files
- **LogRouter** - Category-based log routing with enable/disable per category and JSON persistence
- **LogService** - Service wrapper for logging dynamic objects as JSON via ILogger

## Installation

```bash
dotnet add package SunamoLogging
```

## Logger Implementations

| Logger | Base Class | Purpose |
|---|---|---|
| SunamoLogger | LoggerBase | Production logger via ThisApp.Ordinal |
| DebugLogger | LoggerBase | Debug-only output |
| DummyLogger | LoggerBase | Null object pattern (no-op) |
| SunamoTemplateLogger | TemplateLoggerBase | Template messages via ThisApp.SetStatus |
| DebugTemplateLogger | TemplateLoggerBase | Debug template output to console |
| DummyTemplateLogger | TemplateLoggerBase | Null object pattern (no-op) |
| TypedSunamoLogger | TypedLoggerBase | Typed messages via ThisApp.SetStatus |
| TypedDebugLogger | TypedLoggerBase | Debug-only typed output |
| TypedDummyLogger | TypedLoggerBase | Null object pattern (no-op) |

## Quick Start

```csharp
// Initialize loggers during app startup
InitApp.SetDebugLogger();

// Use the base logger
InitApp.Logger?.WriteLine("Processing item: {0}", itemName);

// Use the template logger
InitApp.TemplateLogger?.SavedToDrive(filePath);
InitApp.TemplateLogger?.Finished("Data import");

// Use the typed logger
InitApp.TypedLogger?.Success("Operation completed");
InitApp.TypedLogger?.Error("Connection failed");
```

## File Logger with ILogger

```csharp
var services = new ServiceCollection();
services.AddLogging(options => options.SetMinimumLevel(LogLevel.Warning));
services.AddSingleton(provider =>
{
    var factory = provider.GetRequiredService<ILoggerFactory>();
    factory.AddFile("MyApp");
    return factory.CreateLogger("General");
});
```

## Target Frameworks

`net10.0;net9.0;net8.0`

## Dependencies

- Microsoft.Extensions.Logging.Abstractions

## Links

- [NuGet](https://www.nuget.org/profiles/sunamo)
- [GitHub](https://github.com/sunamo/PlatformIndependentNuGetPackages)
- [Developer site](https://sunamo.cz)

## License

MIT
