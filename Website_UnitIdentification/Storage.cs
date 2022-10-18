using System.Net.Http.Json;
namespace Website_UnitIdentification;
public class Storage : UnitIdentification.IStorage
{
    private readonly Product.Infomation PI;
    private readonly HttpClient HttpClient;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    private string Domain => PI.Name switch {
        StandardInternal.product.infomation.Name.ForwardOBJECTSOCIAL => "¯\\_(ツ)_/¯", 
        _=> HttpClient.BaseAddress.Host.IndexOf("localhost") !=-1? $"{HttpClient.BaseAddress.Host}:{HttpClient.BaseAddress.Port}": HttpClient.BaseAddress.Host 
    };
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    public async Task<string> Read()=> await HttpClient.GetStringAsync($"https://{Domain}/websitebehind/storage/read");

    public async Task Save(string Token, StandardInternal.unitIdentification.storage.Type Type) => await HttpClient.PostAsJsonAsync($"https://{Domain}/websitebehind/storage/save", new StandardInternal.websiteBehind.Data { Token = Token, Type = Type });
    public async Task<StandardInternal.unitIdentification.storage.Type> Type()=> await HttpClient.GetFromJsonAsync<StandardInternal.unitIdentification.storage.Type>($"https://{Domain}/websitebehind/storage/type");
    public Storage(HttpClient HttpClient, Product.Infomation PI) {
        this.PI = PI;
        this.HttpClient = HttpClient;
    }
}