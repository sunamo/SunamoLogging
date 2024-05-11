namespace SunamoLogger;

public abstract partial class LoggerBase
{
    public void TwoState(bool ret, params string[] toAppend)
    {
        WriteLine(ret.ToString() + AllStrings.comma + string.Join(AllChars.comma, toAppend));
    }
}
