namespace SunamoLogging.Bootstrap;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Result of <see cref="LoggingBootstrap.InitConsoleApp"/>: ready-to-use DI primitives and ILogger.
/// </summary>
public sealed record ConsoleAppContext(
    ServiceCollection Services,
    ServiceProvider Provider,
    ILogger Logger);
