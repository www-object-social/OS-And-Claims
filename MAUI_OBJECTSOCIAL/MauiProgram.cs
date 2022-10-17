using CommunityToolkit.Maui;
namespace MAUI_OBJECTSOCIAL;
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
        builder.Services.AddScoped<UnitIdentification.IStorage, MAUI_UnitIdentification.Storage>();
        builder.Services.AddScoped(x => new Product.Infomation { Name = StandardInternal.product.infomation.Name.OBJECTSOCIAL, Software = Product.infomation.Software.Application });
        builder.Services.AddScoped<UnitIdentification.Engine>();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		#endif
		return builder.Build();
	}
}