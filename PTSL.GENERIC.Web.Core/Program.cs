using System.Net;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.FileProviders;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;
using PTSL.GENERIC.Web.Helper;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation();

// Configure Logging
#if DEBUG
string GetLogFilePath()
{
    string logDirectory = Path.Combine(builder.Environment.ContentRootPath, "..", "web_logs");
    Directory.CreateDirectory(logDirectory);

    string logFileName = $"log-{DateTime.Now:yyyyMMddHHmmss}.txt";
    return Path.Combine(logDirectory, logFileName);
}
builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Error()
    .WriteTo.File(GetLogFilePath(), rollingInterval: RollingInterval.Day));
#endif

// Set API base URL from appsettings.json
URLHelper.ApiBaseURL = builder.Configuration.GetValue<string>("ApiBaseURL") ?? throw new Exception("ApiBaseURL not found in appsettings.json");

// Upload Directory
var uploadDirectory = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", FileHelper.Uploads));
if (Directory.Exists(uploadDirectory) == false)
{
    Directory.CreateDirectory(uploadDirectory);
}

// Configs
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.IOTimeout = TimeSpan.FromHours(2);
    options.Cookie.Name = "SHQTC.Session";
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

// Authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddScheme<CustomAuthenticationScheme, CustomAuthenticationHandler>(CookieAuthenticationDefaults.AuthenticationScheme, options => { });
builder.Services.AddAuthorization();

// Dependency Injection
builder.Services.AddScoped<HttpHelper>();
builder.Services.AddSingleton<FileHelper>();

//builder.Services.AddScoped<IUserRoleService, UserRoleService>(
//options => new UserRoleService(options.GetRequiredService<HttpHelper>()));
builder.Services.AddScoped<IUserRoleService, UserRoleService>();



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();

    // Handle exception and save log to file
#if DEBUG
    app.UseExceptionHandler(error =>
    {
        error.Run(async context =>
        {
            var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerFeature?.Error;

            Log.Error(exception, "An unhandled exception occurred");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "text/plain";

            await context.Response.WriteAsync("An error occurred. Please try again later.");
        });
    });
#endif
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadDirectory),
    RequestPath = $"/{FileHelper.Uploads}"
});
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Dashboard}/{id?}");


app.Run();
