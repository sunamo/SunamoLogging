namespace SunamoLogging.FileLogger;

/// <summary>
/// Extension methods for configuring file logging.
/// </summary>
public static class FileLoggerExtensions
{
    /// <summary>
    /// Adds a file logger to the logger factory with default directory.
    /// </summary>
    /// <param name="factory">The logger factory.</param>
    /// <param name="appName">The application name used for the log directory.</param>
    /// <returns>The logger factory for chaining.</returns>
    public static ILoggerFactory AddFile(this ILoggerFactory factory, string appName)
    {
        factory.AddProvider(FileLoggerProvider.DefaultDirectory(appName));
        return factory;
    }
}