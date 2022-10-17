using System.Net.Http.Json;
namespace Website_UnitIdentification;
public class Storage : UnitIdentification.IStorage
{
    public async Task<string> Read()
    {
        using var HttpClient = HttpClientFactory.CreateClient();
        return await HttpClient.GetStringAsync("https://myos.world/websitebehind/storage/read");
    }

    public async Task Save(string Token, StandardInternal.unitIdentification.storage.Type Type)
    {
        using var HttpClient = HttpClientFactory.CreateClient();
        await HttpClient.PostAsJsonAsync("https://myos.world/websitebehind/storage/save", new StandardInternal.websiteBehind.Data { Token=Token, Type=Type });
    }
    private readonly IHttpClientFactory HttpClientFactory;
    public async Task<StandardInternal.unitIdentification.storage.Type> Type()
    {
        using var HttpClient = HttpClientFactory.CreateClient();
        return await HttpClient.GetFromJsonAsync<StandardInternal.unitIdentification.storage.Type>("https://myos.world/websitebehind/storage/type");
    }
    public Storage(IHttpClientFactory HttpClientFactory) => this.HttpClientFactory = HttpClientFactory;
}