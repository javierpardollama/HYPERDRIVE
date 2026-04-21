using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Ai.Infrastructure.Installers;

/// <summary>
///     Represents a <see cref="SerializerInstaller" /> class.
/// </summary>
public static class SerializerInstaller
{
    /// <summary>
    ///     Installs Serializer
    /// </summary>
    /// <param name="this">Injected <see cref="IServiceCollection" /></param>
    public static void InstallSerializer(this IServiceCollection @this)
    {
        @this.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
    }
}