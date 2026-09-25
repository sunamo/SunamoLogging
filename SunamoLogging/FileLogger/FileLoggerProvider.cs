namespace SunamoLogging.FileLogger;

public class FileLoggerProvider(string path) : ILoggerProvider
{
    public List<LogLevel> LevelsToLog { get; set; } = [LogLevel.Critical, LogLevel.Error, LogLevel.Warning];

    // If set, all log entries go to a single file with this name (joined to path).
    // If null/empty, the legacy per-day file naming yyyy-MM-dd_log.txt is used.
    public string? LogFileName { get; set; }

    public static FileLoggerProvider CustomDirectory(string directory, string appName)
    {
        var logDirectoryPath = Path.Combine(directory, appName);
        FS.CreateFoldersPsysicallyUnlessThere(logDirectoryPath);

        return new FileLoggerProvider(logDirectoryPath);
    }

    public static FileLoggerProvider DefaultDirectory(string appName)
    {
        return CustomDirectory(@"D:\Logs", appName);
    }

    // Deletes all files in the given directory (non-recursive). Intended for wipe-at-startup
    // pattern in console / desktop apps that want a clean log folder per run.
    // Does NOT delete the directory itself. Subdirectories are left untouched.
    public static void WipeDirectory(string directory)
    {
        if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory)) return;
        foreach (var file in Directory.GetFiles(directory))
        {
            try { File.Delete(file); }
            catch (Exception ex) { Console.WriteLine($"Failed to delete log file {file}: {ex.Message}"); }
        }
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(path, LevelsToLog, LogFileName);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
