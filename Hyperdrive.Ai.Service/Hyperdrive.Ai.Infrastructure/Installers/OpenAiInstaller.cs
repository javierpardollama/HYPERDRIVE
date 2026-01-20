using Hyperdrive.Ai.Domain.Settings;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Chat;
using OpenAI.Embeddings;

namespace Hyperdrive.Ai.Infrastructure.Installers;

/// <summary>
///     Represents a <see cref="OpenAiInstaller" /> class.
/// </summary>
public static class OpenAiInstaller
{
    /// <summary>
    ///     Installs Open Ai
    /// </summary>
    /// <param name="this">Injected <see cref="IServiceCollection" /></param>
    /// <param name="settings">Injected <see cref="OpenAiSettings" /></param>
    public static void InstallOpenAi(this IServiceCollection @this, OpenAiSettings @settings)
    {
        @this.AddSingleton(serviceProvider =>
        {
            return new ChatClient(@settings.Model, @settings.Key);
        });

        @this.AddSingleton(serviceProvider =>
        {
            return new EmbeddingClient(@settings.Model, @settings.Key);
        });
    }
}
