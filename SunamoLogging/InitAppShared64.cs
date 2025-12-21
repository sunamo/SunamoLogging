namespace SunamoLogging;

public class InitApp
{
    /// <summary>
    ///     Alternatives are:
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
    public static ILoggerBase? Logger { get; set; }
    public static TypedLoggerBase? TypedLogger { get; set; }
    public static TemplateLoggerBase? TemplateLogger { get; set; }

    #endregion
}