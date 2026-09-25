namespace SunamoLogging.Base;

public abstract class TemplateLoggerBase(Action<TypeOfMessageLogging, string, string[]> writeLineDelegate)
{
    public void SavedToDrive(string path)
    {
        WriteLine(TypeOfMessageLogging.Success, Translate.FromKey(XlfKeys.SavedToDrive) + ": " + path);
    }

    public void TryAFewSecondsLaterAfterFullyInitialized()
    {
        WriteLine(TypeOfMessageLogging.Information, Translate.FromKey(XlfKeys.TryAFewSecondsLaterAfterFullyInitialized));
    }

    public void Finished(string operationName)
    {
        WriteLine(TypeOfMessageLogging.Success, operationName + " - " + Translate.FromKey(XlfKeys.Finished));
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

    public void CopiedToClipboard(string contentDescription)
    {
        WriteLine(TypeOfMessageLogging.Success, contentDescription + " was successfully copied to clipboard.");
    }
    #endregion

    #region Error
    public void CouldNotBeParsed(string entityName, string value)
    {
        WriteLine(TypeOfMessageLogging.Error, entityName + " with value " + value + " could not be parsed");
    }

    public void SomeErrorsOccuredSeeLog()
    {
        WriteLine(TypeOfMessageLogging.Error, Translate.FromKey(XlfKeys.SomeErrorsOccuredSeeLog));
    }

    public void FolderDontExists(string folderPath)
    {
        WriteLine(TypeOfMessageLogging.Error, Translate.FromKey(XlfKeys.Folder) + " " + folderPath + " doesn't exists.");
    }

    public void FileDontExists(string filePath)
    {
        WriteLine(TypeOfMessageLogging.Error, Translate.FromKey(XlfKeys.File) + " " + filePath + " doesn't exists.");
    }
    #endregion

    #region Information
    public void LoadedFromStorage(string description)
    {
        WriteLine(TypeOfMessageLogging.Information, Translate.FromKey(XlfKeys.LoadedFromStorage) + ": " + description);
    }

    public void InsertAsIndexesZeroBased()
    {
        WriteLine(TypeOfMessageLogging.Information, Translate.FromKey(XlfKeys.InsertAsIndexesZeroBased));
    }

    public void UnfortunatelyBadFormatPleaseTryAgain()
    {
        WriteLine(TypeOfMessageLogging.Information, Translate.FromKey(XlfKeys.UnfortunatelyBadFormatPleaseTryAgain) + ".");
    }

    public void OperationWasStopped()
    {
        WriteLine(TypeOfMessageLogging.Information, Translate.FromKey(XlfKeys.OperationWasStopped));
    }

    public void NoData()
    {
        WriteLine(TypeOfMessageLogging.Information, Translate.FromKey(XlfKeys.PleaseEnterRightInputData));
    }

    public void SuccessfullyResized(string fileName)
    {
        WriteLine(TypeOfMessageLogging.Information, Translate.FromKey(XlfKeys.SuccessfullyResizedTo) + " " + fileName);
    }

    #endregion

    public bool AnyElementIsNullOrEmpty(string collectionName, List<string> collection)
    {
        var nullOrEmptyIndexes = CAIndexesWithNull.IndexesWithNullOrEmpty(collection);
        if (nullOrEmptyIndexes.Count > 0)
        {
            var message = Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(), collectionName, nullOrEmptyIndexes);
            if (message is not null)
            {
                WriteLine(TypeOfMessageLogging.Information, message);
            }
            return true;
        }
        return false;
    }

    public void HaveUnallowedValue(string controlName)
    {
        controlName = controlName.TrimEnd(':');
        WriteLine(TypeOfMessageLogging.Appeal, controlName + " have unallowed value");
    }

    public void MustHaveValue(string controlName)
    {
        controlName = controlName.TrimEnd(':');
        WriteLine(TypeOfMessageLogging.Appeal, controlName + " must have value");
    }

    public bool NotEvenNumberOfElements(string collectionName, string[] collection)
    {
        if (collection.Count() % 2 == 1)
        {
            var message = Exceptions.NotEvenNumberOfElements(FullNameOfExecutedCode(), collectionName);
            if (message is not null)
            {
                WriteLine(TypeOfMessageLogging.Error, message);
            }
            return false;
        }
        return true;
    }

    private string FullNameOfExecutedCode() => ThrowEx.FullNameOfExecutedCode();

    private void WriteLine(TypeOfMessageLogging messageType, string message)
    {
        writeLineDelegate(messageType, message, []);
    }

    public bool AnyElementIsNull(string collectionName, string[] collection)
    {
        var nullIndexes = CAIndexesWithNull.IndexesWithNull(collection);
        if (nullIndexes.Count > 0)
        {
            var message = Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(), collectionName, nullIndexes);
            if (message is not null)
            {
                WriteLine(TypeOfMessageLogging.Information, message);
            }
            return true;
        }
        return false;
    }
}
