using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using PTSL.GENERIC.Api.Helpers;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Model.EntityViewModels.SystemUser;
using PTSL.GENERIC.DAL.UnitOfWork;

using Swashbuckle.AspNetCore.SwaggerUI;

using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var postgreConnectionString = builder.Configuration["PostgreSqlConnectionString"];
builder.Services.AddDbContext<GENERICWriteOnlyCtx>(options =>
{
    options.UseNpgsql(postgreConnectionString,
        b => b.MigrationsAssembly(typeof(Program).Assembly.FullName)
    );

#if DEBUG
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
#endif
}, ServiceLifetime.Scoped);

builder.Services.AddDbContext<GENERICReadOnlyCtx>(options =>
{
    options.UseNpgsql(postgreConnectionString,
        b => b.MigrationsAssembly(typeof(Program).Assembly.FullName)
    );

#if DEBUG
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
#endif
}, ServiceLifetime.Scoped);

builder.Services.AddScoped(serviceType: typeof(GENERICUnitOfWork), implementationType: typeof(GENERICUnitOfWork));
builder.Services.AddDependecyResolver();
builder.Services.AddSession();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAutoMapper(typeof(Program));


#region Swagger
builder.Services.Configure<SwaggerUIOptions>(options =>
{
    options.DisplayRequestDuration();
    options.EnablePersistAuthorization();
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "SHQTC API",
        Description = "SHQTC API using .net core",
        TermsOfService = new Uri("http://primetechbd.com"),
        Contact = new OpenApiContact
        {
            Name = "Primetech Solution Ltd",
            Email = "info@primetechbd.com",
            Url = new Uri("http://primetechbd.com"),
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
            },
            Array.Empty<string>()
        }
    });
    c.SchemaFilter<SwaggerExcludeFilter>();
});
#endregion
builder.Services.AddAuthConfig(builder.Configuration);
builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
var uploadDirectory = Path.GetFullPath(Path.Combine(app.Environment.ContentRootPath, "..", FileHelper.Uploads));
Directory.CreateDirectory(uploadDirectory);

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DefaultModelExpandDepth(2);
    c.DefaultModelsExpandDepth(-1);
    c.DisplayOperationId();
    c.DisplayRequestDuration();
    c.EnableDeepLinking();
    c.EnableFilter();
    c.ShowExtensions();
    c.EnableValidator();
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SQTC API V1");
    c.InjectJavascript("/swagger-custom.js");
    c.EnablePersistAuthorization();
});
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
