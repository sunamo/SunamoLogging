namespace SunamoLogging._sunamo.SunamoExceptions;
internal class SH
{
    internal static string NullToStringOrDefault(object n)
    {
        //return NullToStringOrDefault(n, null);
        return n == null ? " " + Consts.nulled : AllStrings.space + n;
    }
}
