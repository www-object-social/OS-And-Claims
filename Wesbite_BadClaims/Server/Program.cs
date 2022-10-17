using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using ProgramData;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped(x => new Product.Infomation { Name = Product.infomation.Name.BadClaims, Software = Product.infomation.Software.Server });
builder.Services.AddSignalR();
builder.Services.AddCors(x => x.AddPolicy("signalr_policy", x => x.AllowAnyMethod().AllowAnyHeader().AllowCredentials().AllowAnyOrigin()));
builder.Services.AddDbContextFactory<ServerStorages.OSAndClaimsContext>(x => x.UseSqlServer(new Func<string>(() => { if (!"database-connection".HaveFile()) { "database-connection".WriteFile("Data Source"); throw new Exception("Error we have created a file in %ProgramData% called database-connection.os-and-claims in which you can place Data source"); } return "database-connection".ReadFile(); })()).UseLazyLoadingProxies());
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
app.MapControllers();
app.UseCors("corsapp");
app.UseCors();
app.MapHub<PongPing.Services>("/PongPing.Services").RequireCors("signalr_policy");
app.MapFallbackToFile("index.html");
app.Run();