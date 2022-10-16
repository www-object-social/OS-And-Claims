using Microsoft.AspNetCore.ResponseCompression;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped(x => new Product.Infomation { Name = Product.infomation.Name.OBJECTSOCIAL, Software = Product.infomation.Software.Server });
builder.Services.AddSignalR();
builder.Services.AddCors(p => p.AddPolicy("corsapp", x =>x.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()));
var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.UseWebAssemblyDebugging();
else
    app.UseHsts();
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseCors("corsapp");
app.UseRouting();
app.MapRazorPages();
app.MapControllers();
app.MapHub<PongPing.Services>("/PongPing.Services");
app.MapFallbackToFile("index.html");
app.Run();