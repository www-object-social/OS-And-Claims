using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using ProgramData;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped(x => new Product.Infomation { Name = Product.infomation.Name.ForwardOBJECTSOCIAL, Software = Product.infomation.Software.Server });
builder.Services.AddSignalR();
builder.Services.AddCors(p => p.AddPolicy("corsapp", x => x.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddDbContextFactory<ServerStorages.OSAndClaimsContext>(x => x.UseSqlServer(new Func<string>(() => {if (!"database-connection".HaveFile()){"database-connection".WriteFile("Data Source");throw new Exception("Error we have created a file in %ProgramData% called database-connection.os-and-claims in which you can place Data source");}return "database-connection".ReadFile();})()).UseLazyLoadingProxies());
builder.Services.AddSession(x => {
    x.IOTimeout = TimeSpan.FromMinutes(60);
    x.IdleTimeout=TimeSpan.FromMinutes(60);
    x.Cookie = new CookieBuilder { HttpOnly=true, IsEssential=true, Name="UI", SecurePolicy= CookieSecurePolicy.Always, Expiration= TimeSpan.FromHours(72), SameSite= SameSiteMode.Lax, MaxAge= TimeSpan.FromHours(72) }; 
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.UseWebAssemblyDebugging();
else
    app.UseHsts();
app.UseSession();
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.UseCors("corsapp");
app.MapHub<PongPing.Services>("/PongPing.Services");
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();