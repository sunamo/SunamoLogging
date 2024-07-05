

namespace SunamoLogging.Base;

public abstract class TemplateLoggerBase
{
    public void SavedToDrive(string v)
    {
        WriteLine(TypeOfMessageLogging.Success, sess.i18n(XlfKeys.SavedToDrive) + ": " + v);
    }

    public void TryAFewSecondsLaterAfterFullyInitialized()
    {
        WriteLine(TypeOfMessageLogging.Information, sess.i18n(XlfKeys.TryAFewSecondsLaterAfterFullyInitialized));
    }

    public void Finished(string nameOfOperation)
    {
        WriteLine(TypeOfMessageLogging.Success, nameOfOperation + " - " + sess.i18n(XlfKeys.Finished));
    }
    public void EndRunTime()
    {
        WriteLine(TypeOfMessageLogging.Ordinal, Messages.AppWillBeTerminated);
    }
    #region Success
    public void ResultCopiedToClipboard()
    {
        WriteLine(TypeOfMessageLogging.Success, "Result was successfully copied to clipboard.");
    }

    public void CopiedToClipboard(string what)
    {
        WriteLine(TypeOfMessageLogging.Success, what + " was successfully copied to clipboard.");
    }
    #endregion
    #region Error
    public void CouldNotBeParsed(string entity, string text)
    {
        WriteLine(TypeOfMessageLogging.Error, entity + " with value " + text + " could not be parsed");
    }
    public void SomeErrorsOccuredSeeLog()
    {
        WriteLine(TypeOfMessageLogging.Error, sess.i18n(XlfKeys.SomeErrorsOccuredSeeLog));
    }
    public void FolderDontExists(string folder)
    {
        WriteLine(TypeOfMessageLogging.Error, sess.i18n(XlfKeys.Folder) + " " + folder + " doesn't exists.");
    }
    public void FileDontExists(string selectedFile)
    {
        WriteLine(TypeOfMessageLogging.Error, sess.i18n(XlfKeys.File) + " " + selectedFile + " doesn't exists.");
    }

    #endregion
    #region Information
    public void LoadedFromStorage(string item)
    {
        WriteLine(TypeOfMessageLogging.Information, sess.i18n(XlfKeys.LoadedFromStorage) + ": " + item);
    }

    public void InsertAsIndexesZeroBased()
    {
        WriteLine(TypeOfMessageLogging.Information, sess.i18n(XlfKeys.InsertAsIndexesZeroBased));
    }
    public void UnfortunatelyBadFormatPleaseTryAgain()
    {
        WriteLine(TypeOfMessageLogging.Information, sess.i18n(XlfKeys.UnfortunatelyBadFormatPleaseTryAgain) + ".");
    }
    public void OperationWasStopped()
    {
        WriteLine(TypeOfMessageLogging.Information, sess.i18n(XlfKeys.OperationWasStopped));
    }
    public void NoData()
    {
        WriteLine(TypeOfMessageLogging.Information, sess.i18n(XlfKeys.PleaseEnterRightInputData));
    }
    /// <summary>
    /// Zmena: metoda nezapisuje primo na konzoli, misto toho pouze vraci retezec
    /// </summary>
    /// <param name="fn"></param>
    public void SuccessfullyResized(string fn)
    {
        WriteLine(TypeOfMessageLogging.Information, sess.i18n(XlfKeys.SuccessfullyResizedTo) + " " + fn);
    }



    /// <summary>
    /// Return true if any will be null or empty
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="nameOfCollection"></param>
    /// <param name="args"></param>
    public bool AnyElementIsNullOrEmpty(Type type, string methodName, string nameOfCollection, List<string> args)
    {
        List<int> nulled = CAIndexesWithNull.IndexesWithNullOrEmpty(args);
        if (nulled.Count > 0)
        {
            WriteLine(TypeOfMessageLogging.Information, Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(t.Item1, t.Item2), nameOfCollection, nulled));
            return true;
        }
        return false;
    }

    public void HaveUnallowedValue(string controlNameOrText)
    {
        controlNameOrText = controlNameOrText.TrimEnd(AllChars.colon);
        WriteLine(TypeOfMessageLogging.Appeal, controlNameOrText + " have unallowed value");
    }
    public void MustHaveValue(string controlNameOrText)
    {
        controlNameOrText = controlNameOrText.TrimEnd(AllChars.colon);
        WriteLine(TypeOfMessageLogging.Appeal, controlNameOrText + " must have value");
    }
    #endregion

    public TemplateLoggerBase(Action<TypeOfMessageLogging, string, string[]> writeLineDelegate)
    {
        _writeLineDelegate = writeLineDelegate;
    }


    static Type type = typeof(TemplateLoggerBase);
    private Action<TypeOfMessageLogging, string, string[]> _writeLineDelegate;

    /// <summary>
    /// Return true if number of counts is odd
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="nameOfCollection"></param>
    /// <param name="args"></param>
    public bool NotEvenNumberOfElements(Type type, string methodName, string nameOfCollection, string[] args)
    {
        if (args.Count() % 2 == 1)
        {
            WriteLine(TypeOfMessageLogging.Error, Exceptions.NotEvenNumberOfElements(FullNameOfExecutedCode(t.Item1, t.Item2), nameOfCollection));
            return false;
        }
        return true;
    }

    Tuple<string, string, string> t => Exc.GetStackTrace2(true);

    private string FullNameOfExecutedCode(object type, string methodName)
    {
        return ThrowEx.FullNameOfExecutedCode(t.Item1, t.Item2);
    }

    private void WriteLine(TypeOfMessageLogging error, string v)
    {
        _writeLineDelegate(error, v, EmptyArrays.Strings);
    }



    /// <summary>
    /// Return true if any will be null
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="nameOfCollection"></param>
    /// <param name="args"></param>
    public bool AnyElementIsNull(Type type, string methodName, string nameOfCollection, string[] args)
    {
        List<int> nulled = CAIndexesWithNull.IndexesWithNull(args);
        if (nulled.Count > 0)
        {
            WriteLine(TypeOfMessageLogging.Information, Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(t.Item1, t.Item2), nameOfCollection, nulled));
            return true;
        }
        return false;
    }
}
