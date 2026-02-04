namespace SunamoLogging.Base;

/// <summary>
/// Base logger class that uses typed message enums instead of plain strings.
/// In difference with LoggerBase, this takes type of message as enum.
/// </summary>
public abstract class TypedLoggerBase
{
    private static Type type = typeof(TypedLoggerBase);
    private Action<TypeOfMessageLogging, string, string[]>? _typedWriteLineDelegate;

    /// <summary>
    /// Initializes a new instance of the TypedLoggerBase class.
    /// </summary>
    /// <param name="typedWriteLineDelegate">The delegate to invoke for writing log messages.</param>
    public TypedLoggerBase(Action<TypeOfMessageLogging, string, string[]> typedWriteLineDelegate)
    {
        _typedWriteLineDelegate = typedWriteLineDelegate;
    }

#if !DEBUG2
    /// <summary>
    /// Initializes a new instance of the TypedLoggerBase class with no delegate.
    /// </summary>
    public TypedLoggerBase()
    {
        _typedWriteLineDelegate = null;
    }
#endif

    /// <summary>
    /// Writes a formatted line as an ordinary message.
    /// Legacy compatibility method for old applications.
    /// </summary>
    /// <param name="format">The format string.</param>
    /// <param name="args">Format arguments.</param>
    public void WriteLineFormat(string format, params string[] args)
    {
        Ordinal(format, args);
    }

    #region Message Type Methods
    /// <summary>
    /// Writes a success message.
    /// </summary>
    /// <param name="text">The message text.</param>
    /// <param name="args">Format arguments.</param>
    public void Success(string text, params string[] args)
    {
        _typedWriteLineDelegate?.Invoke(TypeOfMessageLogging.Success, text, args);
    }

    /// <summary>
    /// Writes an error message.
    /// </summary>
    /// <param name="text">The message text.</param>
    /// <param name="args">Format arguments.</param>
    public void Error(string text, params string[] args)
    {
        _typedWriteLineDelegate?.Invoke(TypeOfMessageLogging.Error, text, args);
    }

    /// <summary>
    /// Writes a warning message.
    /// </summary>
    /// <param name="text">The message text.</param>
    /// <param name="args">Format arguments.</param>
    public void Warning(string text, params string[] args)
    {
        _typedWriteLineDelegate?.Invoke(TypeOfMessageLogging.Warning, text, args);
    }

    /// <summary>
    /// Writes an appeal/request message to the user.
    /// </summary>
    /// <param name="text">The message text.</param>
    /// <param name="args">Format arguments.</param>
    public void Appeal(string text, params string[] args)
    {
        _typedWriteLineDelegate?.Invoke(TypeOfMessageLogging.Appeal, text, args);
    }

    /// <summary>
    /// Writes an ordinary/normal message.
    /// </summary>
    /// <param name="text">The message text.</param>
    /// <param name="args">Format arguments.</param>
    public void Ordinal(string text, params string[] args)
    {
        _typedWriteLineDelegate?.Invoke(TypeOfMessageLogging.Ordinal, text, args);
    }

    /// <summary>
    /// Writes a message with the specified message type.
    /// </summary>
    /// <param name="messageType">The type of the message.</param>
    /// <param name="message">The message to write.</param>
    public void WriteLine(TypeOfMessageLogging messageType, string message)
    {
        switch (messageType)
        {
            case TypeOfMessageLogging.Error:
                Error(message);
                break;
            case TypeOfMessageLogging.Warning:
                Warning(message);
                break;
            case TypeOfMessageLogging.Information:
                Information(message);
                break;
            case TypeOfMessageLogging.Ordinal:
                Ordinal(message);
                break;
            case TypeOfMessageLogging.Appeal:
                Appeal(message);
                break;
            case TypeOfMessageLogging.Success:
                Success(message);
                break;
            default:
                ThrowEx.NotImplementedCase(messageType);
                break;
        }
    }

    /// <summary>
    /// Writes an informational message.
    /// </summary>
    /// <param name="text">The message text.</param>
    /// <param name="args">Format arguments.</param>
    public void Information(string text, params string[] args)
    {
        _typedWriteLineDelegate?.Invoke(TypeOfMessageLogging.Information, text, args);
    }
    #endregion
}