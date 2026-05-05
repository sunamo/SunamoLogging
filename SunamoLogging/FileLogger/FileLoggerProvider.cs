namespace SunamoLogging.FileLogger;

/// <summary>
/// Logger provider for file-based logging.
/// </summary>
public class FileLoggerProvider(string path) : ILoggerProvider
{
    /// <summary>
    /// Gets or sets the log levels that should be logged.
    /// </summary>
    public List<LogLevel> LevelsToLog { get; set; } = [LogLevel.Critical, LogLevel.Error, LogLevel.Warning];

    /// <summary>
    /// Creates a file logger provider with a custom directory.
    /// </summary>
    /// <param name="directory">The base directory for log files.</param>
    /// <param name="appName">The application name (used as subdirectory).</param>
    /// <returns>A new FileLoggerProvider instance.</returns>
    public static FileLoggerProvider CustomDirectory(string directory, string appName)
    {
        var logDirectoryPath = Path.Combine(directory, appName);
        FS.CreateFoldersPsysicallyUnlessThere(logDirectoryPath);

        return new FileLoggerProvider(logDirectoryPath);
    }

    /// <summary>
    /// Creates a file logger provider with the default directory (D:\Logs).
    /// </summary>
    /// <param name="appName">The application name (used as subdirectory).</param>
    /// <returns>A new FileLoggerProvider instance.</returns>
    public static FileLoggerProvider DefaultDirectory(string appName)
    {
        return CustomDirectory(@"D:\Logs", appName);
    }

    /// <summary>
    /// Deletes all files in the given directory (non-recursive). Intended for wipe-at-startup
    /// pattern in console / desktop apps that want a clean log folder per run.
    /// Does NOT delete the directory itself. Subdirectories are left untouched.
    /// </summary>
    /// <param name="directory">Directory whose files should be deleted.</param>
    public static void WipeDirectory(string directory)
    {
        if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory)) return;
        foreach (var file in Directory.GetFiles(directory))
        {
            try { File.Delete(file); }
            catch (Exception ex) { Console.WriteLine($"Failed to delete log file {file}: {ex.Message}"); }
        }
    }

    /// <summary>
    /// Creates a logger instance for the specified category.
    /// </summary>
    /// <param name="categoryName">The category name for the logger.</param>
    /// <returns>A new FileLogger instance.</returns>
    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(path, LevelsToLog);
    }

    /// <summary>
    /// Disposes the logger provider.
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}