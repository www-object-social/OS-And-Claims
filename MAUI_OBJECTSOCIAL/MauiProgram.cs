using CommunityToolkit.Maui;
using System;

namespace MAUI_OBJECTSOCIAL;
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder.UseMauiApp<App>().UseMauiCommunityToolkit();
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddScoped<Unit.IInfomation, MAUI_Unit.Infomation>();
        builder.Services.AddScoped<Progress.Manager>();
        builder.Services.AddScoped<PingPong.Engine>();
        builder.Services.AddScoped(x => new Product.Infomation { Name = Product.infomation.Name.OBJECTSOCIAL, Software = Product.infomation.Software.Application });
		#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		#endif
		return builder.Build();
	}
}