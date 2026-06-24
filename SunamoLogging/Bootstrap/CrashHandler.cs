namespace SunamoLogging.Bootstrap;

using System.Runtime.InteropServices;

public static class CrashHandler
{
    private static string? _crashFile;
    private static bool _registered;

    // Subscribes to AppDomain.UnhandledException and TaskScheduler.UnobservedTaskException.
    // Idempotent — second call updates the target file but does not double-subscribe.
    public static void Register(string crashFilePath)
    {
        _crashFile = crashFilePath;
        var dir = Path.GetDirectoryName(crashFilePath);
        if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
        if (_registered) return;
        AppDomain.CurrentDomain.UnhandledException += OnUnhandled;
        TaskScheduler.UnobservedTaskException += OnUnobserved;
        _registered = true;
    }

    private static void OnUnhandled(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception ex) Write(ex, "UnhandledException");
        else Write(new Exception("Non-Exception object thrown: " + e.ExceptionObject), "UnhandledException");
    }

    private static void OnUnobserved(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        Write(e.Exception, "UnobservedTaskException");
        e.SetObserved();
    }

    private static void Write(Exception ex, string source)
    {
        if (_crashFile is null) return;
        try
        {
            var sb = new StringBuilder();
            sb.AppendLine($"[{DateTime.Now:O}] {source}");
            sb.AppendLine($"App: {AppDomain.CurrentDomain.FriendlyName}");
            sb.AppendLine($"Runtime: {RuntimeInformation.FrameworkDescription}");
            sb.AppendLine($"OS: {RuntimeInformation.OSDescription}");
            sb.AppendLine();
            sb.AppendLine(ex.ToString());
            sb.AppendLine();
            File.AppendAllText(_crashFile, sb.ToString());
        }
        catch (Exception writeEx)
        {
            Console.Error.WriteLine($"CrashHandler failed to write crash log: {writeEx.Message}");
        }
    }
}
