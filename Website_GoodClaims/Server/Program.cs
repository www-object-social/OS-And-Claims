using Microsoft.AspNetCore.ResponseCompression;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped(x => new Product.Infomation { Name = Product.infomation.Name.GoodClaims, Software = Product.infomation.Software.Server });
builder.Services.AddSignalR();
var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.UseWebAssemblyDebugging();
else
    app.UseHsts();
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.MapHub<PongPing.Services>("/PongPing.Services");
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();
