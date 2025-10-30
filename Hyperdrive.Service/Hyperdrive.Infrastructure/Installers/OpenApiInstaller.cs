using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;

namespace Hyperdrive.Infrastructure.Installers;

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
                Title = "HyperDrive.Service"
            });           

            options.DocInclusionPredicate((name, description) => description.GroupName == name);
            options.ResolveConflictingActions(descriptions => descriptions.First());

            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "HyperDrive.Service.xml"));
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header,
                Description = "Jwt Authorization header using the Bearer scheme."
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
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
            @this.UseSwagger();

            @this.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");                
            });
        }
    }
}