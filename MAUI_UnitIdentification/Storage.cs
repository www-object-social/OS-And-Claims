using System.Text;
namespace MAUI_UnitIdentification;
public class Storage : UnitIdentification.IStorage
{
    private readonly string FileName = "UI.OSAndClaims";
    private string Combine(string Path) => System.IO.Path.Combine(Path, this.FileName);
    private void _Write(string Path, string Base64)
    {
        using (FileStream fs = File.OpenWrite(this.Combine(Path)))
        {
            byte[] info = new UTF8Encoding(true).GetBytes(Base64);
            fs.Write(info, 0, info.Length);
            fs.Dispose();
        }
    }
    public async Task<string> Read()
    {
        switch (await Type()) {
            case StandardInternal.unitIdentification.storage.Type.Local:
                return File.ReadAllText(this.Combine(FileSystem.Current.AppDataDirectory));
            case StandardInternal.unitIdentification.storage.Type.Temporarily:
                return File.ReadAllText(this.Combine(FileSystem.Current.CacheDirectory));
            default:
                return "¯\\_(ツ)_/¯";
        }
    }
    public Task Save(string Token, StandardInternal.unitIdentification.storage.Type Type)
    {
        if (Type is StandardInternal.unitIdentification.storage.Type.Local) {
            if (File.Exists(this.Combine(FileSystem.Current.CacheDirectory)))
                File.Delete(this.Combine(FileSystem.Current.CacheDirectory));
            _Write(FileSystem.Current.AppDataDirectory,Token);
            return Task.CompletedTask;
        }
        if (File.Exists(this.Combine(FileSystem.Current.AppDataDirectory)))
            File.Delete(this.Combine(FileSystem.Current.AppDataDirectory));
        _Write(FileSystem.Current.CacheDirectory, Token);
        return Task.CompletedTask;
    }
    public Task<StandardInternal.unitIdentification.storage.Type> Type() => Task.FromResult(File.Exists(this.Combine(FileSystem.Current.AppDataDirectory)) ? StandardInternal.unitIdentification.storage.Type.Local : File.Exists(this.Combine(FileSystem.Current.CacheDirectory)) ? StandardInternal.unitIdentification.storage.Type.Temporarily : StandardInternal.unitIdentification.storage.Type.None);
}