using CommunityToolkit.Maui;
namespace MAUI_MemoryClaims;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().UseMauiCommunityToolkit();
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddHttpClient();
        builder.Services.AddScoped<Unit.IInfomation, MAUI_Unit.Infomation>();
        builder.Services.AddScoped<Progress.Manager>();
        builder.Services.AddScoped<PingPong.Engine>();
        builder.Services.AddScoped(x => new Product.Infomation { Name = Product.infomation.Name.MemoryClaims, Software = Product.infomation.Software.Application });
        builder.Services.AddScoped<UnitIdentification.Engine>();
        builder.Services.AddScoped<UnitIdentification.IStorage, MAUI_UnitIdentification.Storage>();
        #if DEBUG
		    builder.Services.AddBlazorWebViewDeveloperTools();
        #endif
        return builder.Build();
    }
}