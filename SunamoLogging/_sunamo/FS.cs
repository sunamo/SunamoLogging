namespace SunamoLogging._sunamo;

internal class FS
{
    internal static void CreateFoldersPsysicallyUnlessThere(string folderPath)
    {
        ThrowEx.IsNullOrEmpty("folderPath", folderPath);

        if (Directory.Exists(folderPath))
        {
            return;
        }

        List<string> foldersToCreate =
        [
            folderPath
        ];

        string? currentPath = folderPath;
        while (true)
        {
            currentPath = Path.GetDirectoryName(currentPath);

            if (currentPath == null || Directory.Exists(currentPath))
            {
                break;
            }
            foldersToCreate.Add(currentPath);
        }

        foldersToCreate.Reverse();
        foreach (string folder in foldersToCreate)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }
    }
}
