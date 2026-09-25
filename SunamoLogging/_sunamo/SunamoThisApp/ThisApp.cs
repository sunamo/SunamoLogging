namespace SunamoLogging._sunamo.SunamoThisApp;

internal class ThisApp
{
    internal static event Action<TypeOfMessageLogging, string>? StatusSetted;

    internal static void SetStatus(TypeOfMessageLogging messageType, string message, params string[] args)
    {
        var formattedMessage = string.Format(message, args);
        if (formattedMessage.Trim() != string.Empty)
        {
            StatusSetted?.Invoke(messageType, formattedMessage);
        }
    }

    internal static void Ordinal(string message, params string[] args)
    {
        SetStatus(TypeOfMessageLogging.Ordinal, message, args);
    }
}
