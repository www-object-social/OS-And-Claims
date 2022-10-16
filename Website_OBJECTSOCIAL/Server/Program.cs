using Microsoft.AspNetCore.ResponseCompression;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddCors(x => { 
    x.AddPolicy("signalr_policy",x=>x.WithOrigins("https://object.social","https://memory.claims","https://good.claims","https://bad.claims", "https://www.object.social", "https://www.memory.claims", "https://www.good.claims", "https://www.bad.claims", "https://localhost"));

});
builder.Services.AddScoped(x => new Product.Infomation { Name = Product.infomation.Name.OBJECTSOCIAL, Software = Product.infomation.Software.Server });
builder.Services.AddSignalR();
var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.UseWebAssemblyDebugging();
else
    app.UseHsts();
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseCors();
app.UseRouting();
app.MapRazorPages();
app.MapControllers();
app.MapHub<PongPing.Services>("/PongPing.Services");
app.MapFallbackToFile("index.html");
app.Run();