namespace SunamoLogging._sunamo;
internal class SH
{
    internal static string NullToStringOrDefault(object n)
    {

        return n == null ? " " + "(null)" : " " + n;
    }
}