using System;
using System.IO;
using System.Reflection;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Hyperdrive.Service.Extensions
{
    /// <summary>
    /// Represents a <see cref="SwaggerExtension"/> class.
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// Extends Customized Swagger Configuration
        /// </summary>
        /// <param name="this">Injected <see cref="IServiceCollection"/></param>        
        public static void AddCustomizedSwagger(this IServiceCollection @this)
        {
            @this.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
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
    }
}
