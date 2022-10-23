using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Website_MemoryClaims.Client;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("main");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<Unit.IInfomation, Website_Unit.Infomation>();
builder.Services.AddScoped<Progress.Manager>();
builder.Services.AddScoped<PingPong.Engine>();
builder.Services.AddHttpClient();
builder.Services.AddScoped(x => new Product.Infomation { Name = StandardInternal.product.infomation.Name.MemoryClaims, Software = Product.infomation.Software.Browser,
    ISDeveloper =
#if DEBUG
    true
#else
    false
#endif
});
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<UnitIdentification.IStorage, Website_UnitIdentification.Storage>();
builder.Services.AddScoped<UnitIdentification.Engine>();
builder.Services.AddScoped<Helper_UI.Authentication>();
await builder.Build().RunAsync();
