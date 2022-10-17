namespace ProgramData;

public static class IO
{
    public static string StringFolderPathName(this string FileName) => $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}/{FileName}.os-and-claims";
    public static bool HaveFile(this string FileName) => File.Exists(FileName.StringFolderPathName());
    public static string ReadFile(this string FileName)
    {
        if (!HaveFile(FileName)) throw new Exception("Developer mistake! did you use HaveFile");
        return File.ReadAllText(FileName.StringFolderPathName());
    }
    public static void WriteFile(this string FileName, string Content)
    {
        var e = System.Text.Encoding.UTF8.GetBytes(Content);
        using var b = File.Create(FileName.StringFolderPathName(), e.Length);
        b.Write(e);
    }
}