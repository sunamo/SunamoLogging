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
#if DEBUG
        Logger = DebugLogger.Instance;

#endif
        TemplateLogger =
#if DEBUG2 && DEBUG
            DebugTemplateLogger.Instance;
#elif !DEBUG2
            null;
#endif
        TypedLogger =
#if DEBUG2 && DEBUG
            TypedDebugLogger.Instance;
#elif !DEBUG2
            null;
#endif
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