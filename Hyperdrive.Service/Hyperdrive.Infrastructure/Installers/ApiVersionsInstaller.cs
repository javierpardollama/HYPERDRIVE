using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Infrastructure.Installers;

/// <summary>
///     Represents a <see cref="ApiVersionsInstaller" /> class.
/// </summary>
public static class ApiVersionsInstaller
{
    /// <summary>
    ///     Installs Api Versions
    /// </summary>
    /// <param name="this">Injected <see cref="IServiceCollection" /></param>
    public static void InstallApiVersions(this IServiceCollection @this)
    {
        @this.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1.0);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version"));
            })
            .AddMvc() // This is needed for controllers
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
    }
}