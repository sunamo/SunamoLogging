namespace SunamoLogger;
public class ThisApp
{
    public static event Action<TypeOfMessage, string> StatusSetted;

    public static void SetStatus(TypeOfMessage st, string status, params string[] args)
    {
        var format = /*string.Format*/ string.Format(status, args);
        if (format.Trim() != string.Empty)
        {
            if (StatusSetted == null)
            {
                // For unit tests
                //////////DebugLogger.Instance.WriteLine(st + ": " + format);
            }
            else
            {
                StatusSetted(st, format);
            }
        }
    }
    public static void Ordinal(string v, params string[] o)
    {
        SetStatus(TypeOfMessage.Ordinal, v, o);
    }
}