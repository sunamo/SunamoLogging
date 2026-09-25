namespace SunamoLogging._sunamo;

internal class FS
{
    internal static void CreateFoldersPsysicallyUnlessThere(string folderPath)
    {
        ThrowEx.IsNullOrEmpty("folderPath", folderPath);
        Directory.CreateDirectory(folderPath);
    }
}
