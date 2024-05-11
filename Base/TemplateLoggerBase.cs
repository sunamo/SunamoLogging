

namespace SunamoLogger.Base;

public abstract partial class TemplateLoggerBase
{
    public void SavedToDrive(string v)
    {
        WriteLine(TypeOfMessage.Success, sess.i18n(XlfKeys.SavedToDrive) + ": " + v);
    }

    public void TryAFewSecondsLaterAfterFullyInitialized()
    {
        WriteLine(TypeOfMessage.Information, sess.i18n(XlfKeys.TryAFewSecondsLaterAfterFullyInitialized));
    }

    public void Finished(string nameOfOperation)
    {
        WriteLine(TypeOfMessage.Success, nameOfOperation + " - " + sess.i18n(XlfKeys.Finished));
    }
    public void EndRunTime()
    {
        WriteLine(TypeOfMessage.Ordinal, Messages.AppWillBeTerminated);
    }
    #region Success
    public void ResultCopiedToClipboard()
    {
        WriteLine(TypeOfMessage.Success, "Result was successfully copied to clipboard.");
    }

    public void CopiedToClipboard(string what)
    {
        WriteLine(TypeOfMessage.Success, what + " was successfully copied to clipboard.");
    }
    #endregion
    #region Error
    public void CouldNotBeParsed(string entity, string text)
    {
        WriteLine(TypeOfMessage.Error, entity + " with value " + text + " could not be parsed");
    }
    public void SomeErrorsOccuredSeeLog()
    {
        WriteLine(TypeOfMessage.Error, sess.i18n(XlfKeys.SomeErrorsOccuredSeeLog));
    }
    public void FolderDontExists(string folder)
    {
        WriteLine(TypeOfMessage.Error, sess.i18n(XlfKeys.Folder) + " " + folder + " doesn't exists.");
    }
    public void FileDontExists(string selectedFile)
    {
        WriteLine(TypeOfMessage.Error, sess.i18n(XlfKeys.File) + " " + selectedFile + " doesn't exists.");
    }

    #endregion
    #region Information
    public void LoadedFromStorage(string item)
    {
        WriteLine(TypeOfMessage.Information, sess.i18n(XlfKeys.LoadedFromStorage) + ": " + item);
    }

    public void InsertAsIndexesZeroBased()
    {
        WriteLine(TypeOfMessage.Information, sess.i18n(XlfKeys.InsertAsIndexesZeroBased));
    }
    public void UnfortunatelyBadFormatPleaseTryAgain()
    {
        WriteLine(TypeOfMessage.Information, sess.i18n(XlfKeys.UnfortunatelyBadFormatPleaseTryAgain) + ".");
    }
    public void OperationWasStopped()
    {
        WriteLine(TypeOfMessage.Information, sess.i18n(XlfKeys.OperationWasStopped));
    }
    public void NoData()
    {
        WriteLine(TypeOfMessage.Information, sess.i18n(XlfKeys.PleaseEnterRightInputData));
    }
    /// <summary>
    /// Zmena: metoda nezapisuje primo na konzoli, misto toho pouze vraci retezec
    /// </summary>
    /// <param name="fn"></param>
    public void SuccessfullyResized(string fn)
    {
        WriteLine(TypeOfMessage.Information, sess.i18n(XlfKeys.SuccessfullyResizedTo) + " " + fn);
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
            WriteLine(TypeOfMessage.Information, Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(t.Item1, t.Item2), nameOfCollection, nulled));
            return true;
        }
        return false;
    }

    public void HaveUnallowedValue(string controlNameOrText)
    {
        controlNameOrText = controlNameOrText.TrimEnd(AllChars.colon);
        WriteLine(TypeOfMessage.Appeal, controlNameOrText + " have unallowed value");
    }
    public void MustHaveValue(string controlNameOrText)
    {
        controlNameOrText = controlNameOrText.TrimEnd(AllChars.colon);
        WriteLine(TypeOfMessage.Appeal, controlNameOrText + " must have value");
    }
    #endregion
}
