namespace SunamoLogging.Bootstrap;

using Microsoft.Extensions.DependencyInjection;

public sealed record ConsoleAppContext(
    ServiceCollection Services,
    ServiceProvider Provider,
    ILogger Logger);
