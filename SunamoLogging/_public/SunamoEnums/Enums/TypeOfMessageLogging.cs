namespace SunamoLogging._public.SunamoEnums.Enums;

/// <summary>
/// Defines the type of log message.
/// </summary>
public enum TypeOfMessageLogging
{
    /// <summary>
    /// Error message indicating a failure.
    /// </summary>
    Error,

    /// <summary>
    /// Warning message indicating a potential issue.
    /// </summary>
    Warning,

    /// <summary>
    /// Informational message.
    /// </summary>
    Information,

    /// <summary>
    /// Ordinary/normal message.
    /// </summary>
    Ordinal,

    /// <summary>
    /// Appeal/request message to the user.
    /// </summary>
    Appeal,

    /// <summary>
    /// Success message indicating successful operation.
    /// </summary>
    Success
}