using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PTSL.GENERIC.Api.Helpers.Authorize;
using Microsoft.AspNetCore.Authorization;

namespace PTSL.GENERIC.Api.Helpers;

public static class AuthConfig
{
    public static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration)
    {
        // Load all permissions
        services.Scan(scan => scan.FromAssemblyOf<Program>()
            .AddClasses(classes => classes.AssignableTo<IAPIPermission>())
            .AsSelfWithInterfaces()
            .WithSingletonLifetime());

        // Load all permission classes
        var collection = new ServiceCollection();
        collection.Scan(scan => scan.FromAssemblyOf<Program>()
            .AddClasses(classes => classes.AssignableTo<IAPIPermission>())
            .AsSelfWithInterfaces()
            .WithSingletonLifetime());
        var permissionServices = collection.BuildServiceProvider();
        var permissions = permissionServices.GetServices<IAPIPermission>().ToList();

        // Check if permission has duplicate keys
        var duplicates = permissions.GroupBy(x => new { x.PermissionName }).Where(x => x.Count() > 1).SelectMany(x => x);
        if (duplicates.Any())
        {
            throw new Exception($"Duplicate permission found named '{duplicates.First().PermissionName}'");
        }

        // Add Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "bearer";
            options.DefaultChallengeScheme = "bearer";
        }).AddJwtBearer("bearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("Token-Expired", "true");
                    }
                    return Task.CompletedTask;
                }
            };
        });

        services.AddScoped<IAuthorizationHandler, RoleClaimsRequirementHandler>();

        services.AddAuthorization(options =>
        {
            foreach (var permission in permissions)
            {
                options.AddPolicy(permission.PermissionName, policy => policy
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes("bearer")
                    .AddRequirements(new RoleClaimsRequirement(permission.PermissionName, permission.PermissionName)));
            }
        });

        return services;
    }
}
