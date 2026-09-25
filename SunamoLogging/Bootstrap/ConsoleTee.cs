namespace SunamoLogging.Bootstrap;

/// <summary>
/// Mirrors Console.Out and Console.Error to a file in addition to the original console streams.
/// Install once at app startup to capture all Console.WriteLine output to disk.
/// </summary>
public static class ConsoleTee
{
    private static StreamWriter? _fileWriter;
    private static bool _installed;

    /// <summary>
    /// Redirects Console.Out and Console.Error so every write goes to both the original
    /// console stream and the given file. Idempotent — second call is no-op.
    /// </summary>
    public static void Install(string filePath)
    {
        if (_installed) return;
        var dir = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
        _fileWriter = new StreamWriter(new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.Read)) { AutoFlush = true };
        Console.SetOut(new TeeWriter(Console.Out, _fileWriter));
        Console.SetError(new TeeWriter(Console.Error, _fileWriter));
        _installed = true;
    }

    private sealed class TeeWriter(TextWriter primary, TextWriter secondary) : TextWriter
    {
        public override System.Text.Encoding Encoding => primary.Encoding;
        public override void Write(char value) { primary.Write(value); secondary.Write(value); }
        public override void Write(string? value) { primary.Write(value); secondary.Write(value); }
        public override void WriteLine(string? value) { primary.WriteLine(value); secondary.WriteLine(value); }
        public override void WriteLine() { primary.WriteLine(); secondary.WriteLine(); }
        public override void Flush() { primary.Flush(); secondary.Flush(); }
    }
}
