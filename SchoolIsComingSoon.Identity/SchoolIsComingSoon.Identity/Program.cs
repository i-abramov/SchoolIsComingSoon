using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using SchoolIsComingSoon.Identity;
using SchoolIsComingSoon.Identity.Data;
using SchoolIsComingSoon.Identity.Models;
using SchoolIsComingSoon.Identity.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

var connectionString = builder.Configuration.GetConnectionString("DbConnection")
    ?? builder.Configuration["DbConnection"];

var issuerUri = builder.Configuration["IdentityServer:IssuerUri"];
var clientUrl = builder.Configuration["ClientURL"] ?? "https://schooliscomingsoon.ru";

builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<AuthDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/var/www/sics/data-protection-keys"))
    .SetApplicationName("SICS.Identity");

var keyPath = Path.Combine(builder.Environment.ContentRootPath, "tempkey.rsa");

builder.Services.AddIdentityServer(options =>
{
    options.IssuerUri = issuerUri;
    options.KeyManagement.Enabled = true;
})
.AddDeveloperSigningCredential(persistKey: true, filename: keyPath)
.AddAspNetIdentity<AppUser>()
.AddInMemoryIdentityResources(Configuration.IdentityResources)
.AddInMemoryApiResources(Configuration.ApiResources)
.AddInMemoryApiScopes(Configuration.ApiScopes)
.AddInMemoryClients(Configuration.Clients(clientUrl))
.AddProfileService<ProfileService>();

builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<DbInitializer>();
builder.Services.AddScoped<EmailService>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "SchoolIsComingSoon.Identity.Cookie";
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

var stylesPath = Path.Combine(builder.Environment.ContentRootPath, "Styles");
if (Directory.Exists(stylesPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(stylesPath),
        RequestPath = "/styles"
    });
}
app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var sp = scope.ServiceProvider;
    try
    {
        var context = sp.GetRequiredService<AuthDbContext>();
        var initializer = sp.GetRequiredService<DbInitializer>();
        await initializer.InitializeAsync(context);
    }
    catch (Exception ex)
    {
        var logger = sp.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while initializing the database");
    }
}

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.MapDefaultControllerRoute();

var clients = app.Services.GetRequiredService<Duende.IdentityServer.Stores.IClientStore>();
var client = await clients.FindClientByIdAsync("schooliscomingsoon-web-app");
var allow = client?.AllowOfflineAccess ?? false;
app.Logger.LogInformation(">>> RUNTIME Client.AllowOfflineAccess = {AllowOfflineAccess}", allow);

await app.RunAsync();