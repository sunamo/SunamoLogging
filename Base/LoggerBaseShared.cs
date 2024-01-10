namespace SunamoLogger.Base;

public abstract partial class LoggerBase
{
    public void TwoState(bool ret, params string[] toAppend)
    {
        WriteLine(ret.ToString() + AllStringsSE.comma + string.Join(AllCharsSE.comma, toAppend));
    }
}
