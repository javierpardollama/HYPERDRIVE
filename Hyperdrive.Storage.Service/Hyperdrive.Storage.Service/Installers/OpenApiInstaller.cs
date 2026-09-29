using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi;

namespace Hyperdrive.Storage.Service.Installers;

/// <summary>
///     Represents a <see cref="OpenApiInstaller" /> class.
/// </summary>
public static class OpenApiInstaller
{
    /// <summary>
    ///     Installs Open Api
    /// </summary>
    /// <param name="this">Injected <see cref="IServiceCollection" /></param>
    public static void InstallOpenApi(this IServiceCollection @this)
    {
        @this.AddEndpointsApiExplorer();

        @this.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "1.0",
                Title = $"{Assembly.GetExecutingAssembly().GetName().Name}"
            });

            options.DocInclusionPredicate((name, description) => description.GroupName == name);
            options.ResolveConflictingActions(descriptions => descriptions.First());

            var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header,
                Description = "Jwt Authorization header using the Bearer scheme."
            });


            options.AddSecurityRequirement((document) => new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecuritySchemeReference(JwtBearerDefaults.AuthenticationScheme,
                                                       document),
                    ["read", "write"]
                }
            });
        });
    }

    /// <summary>
    ///     Uses Open Api
    /// </summary>
    /// <param name="this">Injected <see cref="WebApplication" /></param>
    public static void UseOpenApi(this WebApplication @this)
    {
        if (@this.Environment.IsDevelopment())
        {
            @this.UseSwagger(options =>
            {
                options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_1;
            });

            @this.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}