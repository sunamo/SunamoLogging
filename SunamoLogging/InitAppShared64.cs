namespace SunamoLogging;

/// <summary>
/// Application initialization helper for setting up loggers.
/// </summary>
public class InitApp
{
    /// <summary>
    /// Sets up debug loggers for the application.
    /// Alternatives are:
    ///     InitApp.SetDebugLogger
    ///     CmdApp.SetLogger
    ///     WpfApp.SetLogger
    /// </summary>
    public static void SetDebugLogger()
    {
        TemplateLogger = DebugTemplateLogger.Instance;
        TypedLogger = TypedDebugLogger.Instance;
    }

    #region Must be set during app initializing
    /// <summary>
    /// Gets or sets the base logger instance. Must be set during application initialization.
    /// </summary>
    public static ILoggerBase? Logger { get; set; }

    /// <summary>
    /// Gets or sets the typed logger instance. Must be set during application initialization.
    /// </summary>
    public static TypedLoggerBase? TypedLogger { get; set; }

    /// <summary>
    /// Gets or sets the template logger instance. Must be set during application initialization.
    /// </summary>
    public static TemplateLoggerBase? TemplateLogger { get; set; }

    #endregion
}