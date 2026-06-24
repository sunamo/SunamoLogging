namespace SunamoLogging._sunamo;

internal class SH
{
    internal static string NullToStringOrDefault(object value)
    {
        return value == null ? " " + "(null)" : " " + value;
    }
}
