namespace SunamoLogging._sunamo.SunamoThisApp;

/// <summary>
/// Application status management class.
/// </summary>
internal class ThisApp
{
    /// <summary>
    /// Event raised when application status is set.
    /// </summary>
    internal static event Action<TypeOfMessageLogging, string>? StatusSetted;

    /// <summary>
    /// Sets the application status with the specified message type and formatted message.
    /// </summary>
    /// <param name="messageType">The type of the message.</param>
    /// <param name="message">The message format string.</param>
    /// <param name="args">The format arguments.</param>
    internal static void SetStatus(TypeOfMessageLogging messageType, string message, params string[] args)
    {
        var formattedMessage = string.Format(message, args);
        if (formattedMessage.Trim() != string.Empty)
        {
            if (StatusSetted != null)
            {
                StatusSetted(messageType, formattedMessage);
            }
        }
    }

    /// <summary>
    /// Sets an ordinary/normal status message.
    /// </summary>
    /// <param name="message">The message format string.</param>
    /// <param name="args">The format arguments.</param>
    internal static void Ordinal(string message, params string[] args)
    {
        SetStatus(TypeOfMessageLogging.Ordinal, message, args);
    }
}